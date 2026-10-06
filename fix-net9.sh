#!/usr/bin/env sh
# SharpQuest: bring a repository made in session 1 up to date, so it builds on a lab PC's .NET 9.
#
# Run it INSIDE your repository folder (the one with SharpQuest.slnx), in Rider's terminal or any Linux/macOS terminal:
#
#   curl -fsSL https://raw.githubusercontent.com/x86bogdan/fcpl-2026/main/fix-net9.sh | sh
#
# It does what `sq update` does, without needing sq to run first: it downloads the course's official
# files and puts them in place. It never touches src/, tests/ or anything else you wrote.
# Needs curl or wget, and unzip or python3. Plain POSIX sh.

set -e

if [ ! -f SharpQuest.slnx ] || [ ! -f sharpquest.json ]; then
  echo "Run this inside your SharpQuest repository: the folder with SharpQuest.slnx in it." >&2
  exit 1
fi

value() { sed -n "s/.*\"$1\"[[:space:]]*:[[:space:]]*\"\([^\"]*\)\".*/\1/p" sharpquest.json | head -n 1; }
UPSTREAM=$(value upstream)
BRANCH=$(value branch)
[ -n "$BRANCH" ] || BRANCH=main
case "$UPSTREAM" in
  ""|*CHANGE-ME*) echo "sharpquest.json has no course repository in it. Ask your instructor for its name." >&2; exit 1 ;;
esac

TMP=$(mktemp -d 2>/dev/null || mktemp -d -t sqfix)
trap 'rm -rf "$TMP"' EXIT
ZIP="$TMP/official.zip"

if [ -n "$SQ_ZIP" ]; then
  cp "$SQ_ZIP" "$ZIP"                                  # only for testing this script
else
  URL="https://codeload.github.com/$UPSTREAM/zip/refs/heads/$BRANCH"
  echo "Downloading $URL ..."
  if command -v curl >/dev/null 2>&1; then curl -fsSL "$URL" -o "$ZIP"
  elif command -v wget >/dev/null 2>&1; then wget -q "$URL" -O "$ZIP"
  else echo "Neither curl nor wget is installed. Use way 2 in DOTNET-9.md (download the ZIP in the browser)." >&2; exit 1
  fi
fi

if command -v unzip >/dev/null 2>&1; then unzip -q "$ZIP" -d "$TMP/x"
elif command -v python3 >/dev/null 2>&1; then python3 -m zipfile -e "$ZIP" "$TMP/x"
else echo "Neither unzip nor python3 is installed. Use way 2 in DOTNET-9.md (download the ZIP in the browser)." >&2; exit 1
fi

# GitHub zips hold one top-level folder, e.g. SharpQuest-2026-main/
OFFICIAL=$(find "$TMP/x" -mindepth 1 -maxdepth 1 -type d | head -n 1)

# The same list as Repo.Managed in sq.cs.
for path in contracts reference catchup \
            Directory.Build.props Directory.Build.targets Directory.Packages.props global.json \
            sq.cs sq.cmd sq.sh .github/workflows/ci.yml tools/sq/sq.csproj .config/dotnet-tools.json; do
  [ -e "$OFFICIAL/$path" ] || continue
  if [ -d "$OFFICIAL/$path" ]; then
    rm -rf "./$path"
    mkdir -p "./$path"
    cp -R "$OFFICIAL/$path/." "./$path/"
  else
    mkdir -p "./$(dirname "$path")"
    cp "$OFFICIAL/$path" "./$path"
  fi
  echo "  updated $path"
done

echo
echo "Done. Now run:  sh sq.sh test"
echo "Then save it to GitHub: git commit and push, or, without git, sh sq.sh upload (it says what to do)."
