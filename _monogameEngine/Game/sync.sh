# Syncs all assets to the clients
sync() {
    PROJECT_DIR=$1
    rsync -r Assets/ $PROJECT_DIR/Content --delete --progress --exclude=/*.mgcb
}

sync Client.DesktopGL
