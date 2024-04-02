# CDOpen
C# App made to open the cash drawer connected to ESC/POS Thermal Printers, from a web interface.


## Installation

To use this project you will need to create a registry to handle the click action on your web app, on the folder registry there is a registry file that will create the necesary keys you can just edit the directory where the app will reside and the name of the protocol. You can also create the protocol from scratch.

[How to Create a Custom Protocol on Windows](https://stackoverflow.com/questions/80650/how-do-i-register-a-custom-url-protocol-in-windows)


### Command

For the command to be recognized you need to use the *href* attribute, the command conssists of a single string where the commands are separated by the *@* sign. 
**href="ThermalHLP:EXEC_TYPE@PRINER_NAME@CD#@OPT1@OPT2"**

- **ThermalHLP:** This is the name assigned to the protocol and must be the same one registered in the registry, otherwise the call to the application will not be successful. 
- **EXEC_TYPE** The mode by which the cash drawer is required to be opened is specified, the options are as follows
    - *AUTO* It means that a commonly used command will be used to open the cash drawer, there are other options for this parameter.
    - *EASY* An internal library will be used to open the cash drawer.
    - *MANUAL* Will require you to specify the specific pulse length for opening the cash drawer.

- **PRINER_NAME** The name of the printer compatible with ESC/POS commands, it cannot contain spaces.
- **CD#** Certain printers have more than 1 port for the cash drawer, you must specify which one the opening pulse is sent to.
- **OPT1 & OPT2** Only required when using the *MANUAL* option in the *EXEC_TYPE* argument. A more detailed explanation can be found in the following resource (refers to parameters *t1* and *t2*). [How to program a cash drawer to open using Escape commands](https://www.beaglehardware.com/howtoprogramcashdrawer.html).

#### Examples of the Command
```
href="ThermalHLP:EASY@PRINTER1@0@@"

href="ThermalHLP:AUTO@PRINTER1@0@@"

href="ThermalHLP:MANUAL@PRINTER1@1@25@250"
```
