# Solutia-Seed-Angular-ZurbApp
Seed app containing Angular JS, Zurb for Aps

This seed app contains the following Structure:

1. Application
2. Design
3. Documents

___

##Application Folder##
The Application folder contains these subfolders:

1. **Client** folder houses any codebases that are client centric.  Typically, this would be HTML applications.
2. **Database** folder houses seed databases.
3. **Server** folder houses any codebases that are server centric.  Typicall, this would be WebAPI applications.
 
This seed app supports 2 different types of backends:

1. SeedApp
2. SeedApp for Tenants
 
####SeedApp vs SeedApp for Tenants####
There are 2 types of back-ends supported:

1. **Seed App**.  This particular type of backend is the most common where you have a single client.  In other words, the data managed in the database is intended for a single client.  The stored procedures for this type of implementation do not rely upon TenantId to distinguish the correct context.  All of the plumbing internal to the WebAPI does not rely upon a Tenant Context.  
2. **Seed App for Multiple Tenants**.  This particular type of backend is more specialized.  This type of backend is used for an application like Guess.A.List where multiple tenants share the same web services and database.  A tenant context is established in the WebAPI codebase and a tenant key is submitted to every stored procedure so the correct context is used when retrieving data.

#### \Application\Client Folder ####
This Seed App contains an AngularJS application utilizing Zurb Foundation for Apps for the CSS Framework.  This Client Application folder uses the following technologies:

1. Angular JS
2. Zurb Foundation for Apps (http://foundation.zurb.com/apps/docs/)
3. Grunt is used to prepare the codebase for different build paths.
4. PhoneGap Developer App (http://app.phonegap.com/)

Grunt supports the following build paths:

1. **grunt build:dev** produces a codebase in '..\Client\SeedApp\_builds\web\dev' folder.  You should have your local IIS (local.seedapp.com) point to this folder.  Note, grunt build:dev does not minify any JS code.
2. **grunt build:prod** produces a codebase in '..\Client\SeedApp\_builds\web\prod' folder and minifies all CSS and JS files.  The Index.html page is also modified to use the minified files.
3. **grunt build:phonegap** produces a codebase in '..\Client\SeedApp\_builds\phonegap\cordova-cli\[app name sub-folder\www' folder.  This folder is where the contets of the 'app' is built and deployed.  You would use Cordova CommandLine Interface (cli) for manually doing a build for iOS, Android, etc.
4. **grunt build:phonegapbuild** produces a codebase in '..\Client\SeedApp\_builds\phonegap\build' folder.  This folder is used in PhoneGap Build Service for generating iOS and Android apps.  If you are going to need this functionality, then you need to create a secondary GitHub repo to this folder.  After running **grunt build:phonegapbuild**, you check in the changes to your secondary GitHub repo which also serves as the source for PhoneGap Build Service.  Once you check in your changes to your secondary GitHub repo, then you update PhoneGap Build Service from your secondary GitHub repo and initiate a new iOS/Android build.
5. **grunt build:phonegapserve** produces a codebase in '..\Client\SeedApp\_builds\phonegap\serve' folder.  This is used to test your app using PhoneGap Developer App.  After running **grunt build:phonegapserve**, in your command prompt, navigate to '..\Client\SeedApp\_builds\phonegap\serve' folder.  Run **phonegap serve** command to launch the app in the PhoneGap Developer App on your mobile device.


#### \Application\Database Folder ####

The Database folder housing initial database backup files you can use to start the database supporting this application.  THere are 2 databases to choose from:

1. SeedApp.bak.  This database does not contain a multiple tenant structure.  See Security Database Diagram.
2. SeedAppTenant.bak.  This database **does** contain multiple tenant structure.  See Security Database Diagram.  The multiple tenant structure is pervasive through all stored procedures.


#### \Application\Server Folder ####

1. Web API App for a single client (Application/Server/SeedApp.Service)
2. Web API App for a multiple tenants (Application/Server/SeedApp.Service)


##Design Folder##
The Design folder is intended to be a scratchpad of sorts of design elements for the application.



##Documents Folder##
The purpose of the Documents folder is to contain any ancillary support documents for the application. These items may/may not need to be part of GitHub
