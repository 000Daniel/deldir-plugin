# deldir plugin - command
    This plugin allows you to delete entire directories, and it is compatible with elevated privileges. <br />
    Alternatively this plugin allows you to choose whether you want to delete only files or only folders. <br />
    You also have the option to delete a single file. <br />
    This plugin is a terminal command. <br />
<br />
![image](https://user-images.githubusercontent.com/90350173/149348626-b415577b-eafe-48f3-b7b8-e2a6be71899a.png)
<br />
<br />
## Usage
    Available Flags: <br />
        `-q, --quiet              don't print folder/file tree` <br />
        `-i, --information       display extra information` <br />
        `-b, --basedirectory     delete the base directory` <br />
        `--fol, --foldersonly    delete only folders` <br />
        `--fil, --filesonly      delete only files` <br />
    Examples: <br />
        `deldir` <br />
        `deldir -i -b` <br />
        `deldir Desktop/folder` <br />
        `deldir Desktop/folder -i -b` <br />
        `deldir Desktop/folder -q -b` <br />
        `deldir Desktop/folder --fil` <br />
<br />
<br />
![image](https://user-images.githubusercontent.com/90350173/149345745-ee2995b1-92bb-4587-b2bb-fe8f3ba4b6ca.png)
<br />
<br />
## Note before installing
    The auto install script uses: sudo. <br />
    This script was tested on EndeavourOS(Arch-based), but it should work on other linux systems. <br />
    I do not take any responsibility for any kind of damage this script might cause, <br />
    use it at your own risk.<br />
<br />
<br />
## Installer
    download the `installer.sh` file.<br />
    unpack it in the same directory as the other files. <br />
    enter the next commands into the terminal: <br />
    `sudo chmod +x installer.sh`<br />
    `./installer.sh`<br />
    🞄 after the script installs you should be able to use the deldir command.<br />
<br />
## Manual-Install
    download and unpack the files.<br />
    run in terminal: <br />
    `sudo cp deldir /usr/bin/` <br />
    `sudo cp deldir.pdb /usr/bin/` <br />
    `sudo chmod +x /usr/bin/deldir` <br />
    now you should be able to run the 'deldir' command. <br />
<br />
<br />
<br />
```diff
- created by https://github.com/000Daniel
```
Publish/Release dates: 13.01.2022
