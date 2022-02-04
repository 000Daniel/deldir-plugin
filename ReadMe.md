# deldir plugin - command
    This plugin allows you to delete entire directories, and it is compatible with elevated privileges. <br />
    Alternatively this plugin allows you to choose whether you want to delete only files or only folders. <br />
    You also have the option to delete a single file. <br />
    This plugin is a terminal command. <br />
<br />
![deldir_icon_v2](https://user-images.githubusercontent.com/90350173/152388050-206c530a-65b7-46fe-8320-337451f9b3b1.png)
<br />
<br />
## Usage
    Available Flags: <br />
        `-q, --quiet             don't print folder/file tree` <br />
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
![image](https://user-images.githubusercontent.com/90350173/152392515-d915103a-a575-4366-92db-58d03ec2c0a2.png)
<br />
<br />
## Note before installing
    This script and the plugin should work on any systems with or without dotnet installed. <br />
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
## Change log:
### 1.3
    • Added a better 'tree' preview of folders and files. <br />
    • Added a cleaner 'current' directory display. <br />
    • Added back the 'Version' text in `-h`. <br />
    • Updated the `installer.sh` file. <br />
    • The plugin will now sort the files and the folders. <br />
### 1.2
    • Fixed more issues regarding long directories not displaying properly. <br />
    • Ported this project to use my Argument Parser 3.2 instead of 2.0. <br />
    • Changed a bit the color scheme. <br />
    • Optimized some of the source code(to be more readable). <br />
    • Added more comments in the source code. <br />
### 1.1
    • Fixed some issues regarding long directories not displaying properly. <br />
    • Added more exceptions with new Error messages. <br />
    • Added a 'Version' text in `-h` <br />
<br />
<br />
<br />
<br />
<br />
This software was programmed in Visual Studio Code (.Net 6.0).
> Note: This is an early iteration of this plugin, so expect limitations with this plugin.
<br />

```diff
- created by https://github.com/000Daniel
```
Publish/Release dates: 13.01.2022 <br />
