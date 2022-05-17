[ ! -d TestApp/libs ] && mkdir TestApp/libs

cd rust_dll
cargo build
cp target/debug/rust_dll.dll ../TestApp/libs/.
