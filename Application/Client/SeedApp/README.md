#Seed App#
this is an AngularJS/Zurb Foundation for Apps Seed App


This Seed App uses the following technology:

1. AngularJS (specifically, UI Router.  see https://github.com/angular-ui/ui-router/wiki)
2. Zurb Foundation for Apps. (http://foundation.zurb.com/apps/docs/)
3. Grunt for managing deployments

##Notes##
##Michael de Vera 12-10-2015 - You have to get the latest npm installed localy to your machine.  This is somthing you should do first.
##Especially if you are installing this seed application onto a machine that has a new OS installed.
##In addition, you will need to get grunt installed by using the dos command of: install -s grunt-cli (no spaces between grunt dash and cli)!

##Getting Started##
You will need to do the following to get started using this SeedApp:

1. Copy the entire folder to your target location in your file structure
2. Rename 'SeedApp' to your project name.  You will need to modify the VisualStudio Solution (sln) file to point to the projects in the new folder structure.  Likewise, you will need to rename each project and associated project file (.csproj)
3. Basically, do a global search and replace 'SeedApp' with your project name.
4. Special note: be sure to modify NameSpaces in C# code as well as AngularJS application space from 'SeedApp' to your project name.
5. Run npm install from your command line to install all node_modules needed for this app (so Grunt will work)

___
##Deployments##
This Seed App does the following deployments:

1. To Dev
2. To Prod
3. To Phonegap
4. To Phonegap Build
5. To Phonegap Developer App

###Deployments To Dev###
*grunt command: grunt build or grunt build:dev*

This is the default build command in grunt.

Deployments to dev consists of deploying the project structure to '_builds/web/dev'.  This is the file location that your IIS site on your local machine should be configured to point to.

Grunt uses 'Watch' to monitor changes to LESS, CSS, HTML, JS, and Index.html source code in 'app' folder.  Whenever you modify any of these file types, Grunt Watch will fire grunt build:dev which will deploy the application to '_builds/web/dev'.  You then navigate to your site in a browser to see test your development work.


###Deployments To Prod###
*grunt command: grunt build:prod*

This deployment path redies the codebase for a deployment to a production environment.  This process minifies CSS and JavaScript files and combines them into as few as possible HTTP requests for performances reasons.



###Deployments To Phonegap###
*grunt command: grunt build:phonegap*

This deployment option is used when you need to build this application using Cordova CLI or a native build (Xcode or Eclipse).  This is a manual process.

Check out this reference: http://docs.phonegap.com/en/edge/guide_cli_index.md.html#The%20Command-Line%20Interface

You first must use cordova cli to create the file structure used by cordova cli to do builds.  Do the following:

1. navigate to SeedApp\_builds\phonegap\cordova-cli  (should be an empty folder)
2. in command prompt, run:

'cordova create Seed-App com.seedapp.local SeedApp'  

(remember to change from SeedApp to your project name).  

This will generate a file structure used in cordova cli projects.  Inside the generated 'Seed-App' folder, you will find a 'www' folder.   This 'www' folder is where 'grunt build:phonegap' copies your project's source code from the 'app' folder.  Cordova cli and native deployments (Xcode, Eclipse) will use the source code copied into 'SeedApp\_builds\phonegap\cordova-cli\Seed-App\www' for building the app into mobile distributable files



###Deployments To Phonegap Build###
*grunt command: grunt build:phonegapbuild*

This deployment option readies the codebase for deployment to Phonegap Build Cloud Service.  This process minifies CSS and JavaScript files and combines them into as few as files possible.

You will need to create a separate GitHub repository that points to this folder so after running 'grunt build:phonegapbuild' you can check in the deployed codebase and initiate PhoneGap Build Service (which uses your separate GitHub Repository for it's source).

###Deployments To Phonegap Developer App###
*grunt command: grunt build:phonegapserve*

This deployment option readies the codebase for deployment to Phonegap Developer App (http://app.phonegap.com/).  The Phonegap Developer App is a 3rd party tool.  You can download the Phonegap Developer App onto your mobile device.  You can load your app from your command line onto the mobile device through your network.

After running 'grunt build:phonegapserve', run cd '_builds/phonegap/serve' (navigate to the codebase deployed to this folder) and run 'phonegap serve'.  

'phonegap serve' is the command to initiate Phonegap Developer App.  It creates and opens a port to this folder and then loads the app into the Phonegap Developer App loaded on your device.