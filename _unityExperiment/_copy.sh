# Copies all code + assets to Unity

CODE=CoreGame/App
CODE_DEST=AlteredResidentClient/Assets
ASSETS=Assets
ASSETS_DEST=AlteredResidentClient/Assets


function makeWarning () {
	rm $2
	echo "This folder is copied from ${1} and changes should not be made here." >> $2
}

# Copy code
rsync -vr --exclude 'bin' --exclude 'obj' --exclude *.csproj $CODE $CODE_DEST
makeWarning $CODE $CODE_DEST/App/WARNING.md

# Copy assets
rsync -vr $ASSETS $ASSETS_DEST
makeWarning $ASSETS $ASSETS_DEST/Assets/WARNING.md
