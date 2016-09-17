module.exports = (grunt) ->
    "use strict"

    # clean target build directory
    _clean = 
        dev:
            src: ['<%= paths.build %>/web/dev/**/*']
            options:
                 force: true                 
        cordova:
            src: ['<%= paths.build %>/mobile/cordova-cli/<%= projectVariables.cordovaName %>/www/**/*']
            options:
                 force: true                                  
        cordovacli:
            src: ['<%= paths.build %>/mobile/cordova-cli/**/*']
            options:
                 force: true                                  
        phonegapbuild:
            src: ['<%= paths.build %>/mobile/phonegap-build/<%= projectVariables.phonegapBuild %>/**/*'
                 '!<%= paths.build %>/mobile/phonegap-build/<%= projectVariables.phonegapBuild %>/.git']            
            options:
                 force: true
        phonegapserve:
            src: ['<%= paths.build %>/mobile/phonegap-serve/**/*']
            options:
                 force: true
        prod:
            src: ['<%= paths.build %>/web/prod/**/*']
            options:
                 force: true
             
    # concat css or js files
    _concat =
        # combines all previously minified CSS files into a single stylesheet
        # (this should only be run for PROD environments)    
        css:
            nonull: true
            src: ['app/css/font-awesome.min.css'
                  'app/css/foundation-apps.min.css'
                  'app/css/app.min.css']
            dest: '_builds/<%= grunt.task.current.args[0] %>/css/styles.min.css'
        js:
            nonull: true
            files:
                '_builds/<%= grunt.task.current.args[0] %>/js/app.js':			['app/js/index.js', 'app/js/config/main.js', 'app/js/config/siteConfig.js', 'app/js/config/exception-handler.js']
                '_builds/<%= grunt.task.current.args[0] %>/js/controllers.js':	'app/js/controllers/**/*.js'
                '_builds/<%= grunt.task.current.args[0] %>/js/directives.js':	'app/js/directives/**/*.js'
                '_builds/<%= grunt.task.current.args[0] %>/js/filters.js':		'app/js/filters/**/*.js'
                '_builds/<%= grunt.task.current.args[0] %>/js/models.js':		'app/js/models/**/*.js'
                '_builds/<%= grunt.task.current.args[0] %>/js/services.js':		'app/js/services/**/*.js'            
        statesJs:
            nonull: true
            files:
                '_builds/<%= grunt.task.current.args[0] %>/js/states.js':	'app/states/**/*.js'
                                   
                
    # copy assorted files (fonts, stylesheets, images, partials, states, scripts) to target build directory
    _copy =
		# for phonegap, target root folder (not app folder)
        app_root_phonegap_build_serve:
            expand: true
            cwd: 'app'
            src: ['*.html', '!index.html','!noJavaScript.html', 'config.xml', 'favicon.ico', 'icon.png', 'splash.png', 'robots.txt']
            dest: '_builds/<%= grunt.task.current.args[0] %>'

		# for phonegap using cordova CLI (DO NOT INCLUDE CONFIG.XML)
        app_root_phonegap:
            expand: true
            cwd: 'app'
            src: ['*.html', '!index.html','!noJavaScript.html', '!config.xml', 'favicon.ico', 'icon.png', 'splash.png', 'robots.txt']
            dest: '_builds/<%= grunt.task.current.args[0] %>'

		# for phonegap using cordova CLI - config.xml
        phonegap_config:
            expand: true
            cwd: 'app'
            src: ['config.xml']
            dest: '_builds/<%= grunt.task.current.args[0] %>'

        # copy stylesheets to target build directory
        css:
            expand: true
            cwd: 'app/css'
            src: ['*.css']
            dest: '_builds/<%= grunt.task.current.args[0] %>/css'
                
        # copy fonts to build directory
        fonts:
            expand: true
            cwd: 'app/css/fonts'
            src: ['**/*.*']
            dest: '_builds/<%= grunt.task.current.args[0] %>/css/fonts'
            filter: 'isFile'

        # copy images to target build directory
        images:
            expand: true
            cwd: 'app/css/images'
            src: ['**/*.*']
            dest: '_builds/<%= grunt.task.current.args[0] %>/css/images'
            filter: 'isFile'

        # copy just index.html
        index:
            expand: true
            cwd: 'app'
            src: 'index.html'
            dest: '_builds/<%= grunt.task.current.args[0] %>'
            
        # copy partials to target build directory
        partials:
            expand: true
            cwd: 'app/partials'
            src: ['**/*.html']
            dest: '_builds/<%= grunt.task.current.args[0] %>/partials'

        # copy res to target build directory
        res_phonegap:
            expand: true
            cwd: 'res'
            src: ['**/*']
            dest: '_builds/<%= grunt.task.current.args[0] %>/res'
            
        # copy scripts to target build directory
        scripts:
            expand: true
            cwd: 'app'
            src: ['lib/modernizr/modernizr.js'
                  'lib/jQuery/jquery-2.1.3.min.js'                  
                  'lib/foundation/fastclick.js'
                  'lib/angular/angular.min.js'
                  'lib/angular/angular-animate.min.js'
                  'lib/angular/angular-debounce.min.js'
                  'lib/angular/angular-route.min.js'
                  'lib/angular/angular-sanitize.min.js'
                  'lib/angular/angular-touch.min.js'
                  'lib/angular-ui/angular-adaptive-templating.js'
                  'lib/angular-ui/angular-ui-router.min.js'
                  'lib/foundation/foundation-apps.min.js'
                  'lib/foundation/foundation-apps-templates.js'
                  'lib/matchMedia/matchmedia-ng.js'
                  'lib/hammer.min.js'
                  'lib/livestamp.min.js'
                  'lib/moment.min.js']
            dest: '_builds/<%= grunt.task.current.args[0] %>'

        # copy states to target build directory
        statesHtml:
            expand: true
            cwd: 'app/states'
            src: ['**/*.html']
            dest: '_builds/<%= grunt.task.current.args[0] %>/states'
                
        # copy web.config to target build directory
        web_config:
            expand: true
            cwd: 'app'
            src: ['web.config']
            dest: "_builds/<%= grunt.task.current.args[0] %>"                
                
    # imageMin -> copy and minify all local images to target build directory
    _imageMin = exec:
        options: optimizationLevel: 3
        expand: true
        src: ['app/css/images/**/*.{png,jpg,gif,svg}']
        dest: '_builds/<%= grunt.task.current.args[0] %>/css/images'
        filter: 'isFile'
                
                
    # less compilation
    _less =
        options: ieCompat: false

        # compile less w/ minification
        dev:
            options: compress: false
            files: "app/css/app.min.css": "app/css/less/app.less"
                            
    # processHtml <-- modifies index.html to replace tagged sections with production specific values
    _processHtml =
        options: strip: true
        dev:files: '_builds/<%= grunt.task.current.args[0] %>/index.html': ['app/index.html']
        prod:files: '_builds/<%= grunt.task.current.args[0] %>/index.html': ['app/index.html']
        cordova:files: '_builds/<%= grunt.task.current.args[0] %>/index.html': ['app/index.html']         
        phonegapbuild:files: '_builds/<%= grunt.task.current.args[0] %>/index.html': ['app/index.html'] 
        phonegapserve:files: '_builds/<%= grunt.task.current.args[0] %>/index.html': ['app/index.html']                                                  
                 
                 
#    # robocopy
#    _robocopy = 
#        phonegapbuild:
#            options:
#                source: ['phonegap']
#                destination: '<%= paths.build %>/mobile/phonegap-build/'
#                copy: 
#                    subdirs:true
#                    emptySubdirs:true
#                    mirror: false
#                retry: wait: 1, count: 1                
#        phonegapserve:
#            options:
#                source: ['phonegap']
#                destination: '<%= paths.build %>/mobile/phonegap-serve/'
#                copy: 
#                    subdirs:true
#                    emptySubdirs:true
#                    mirror: false
#                retry: wait: 1, count: 1
#



    # uglify javascript (todo: at some point need to make these lists dynamic based on sections defined in the index.html)
    _uglify =
        options: preserveComments: false, mangle: false
        
        # head js files
        head: files:
            '_builds/<%= grunt.task.current.args[0] %>/js/scripts.head.min.js':
                ['app/lib/modernizr/modernizr.js'
                'app/lib/jQuery/jquery-2.1.3.min.js']
                 
        # body js files
        body: files:
            '_builds/<%= grunt.task.current.args[0] %>/js/scripts.body.min.js':
                ['app/lib/foundation/fastclick.js'
                 'app/lib/angular/angular.min.js'
                 'app/lib/angular/angular-animate.min.js'
                 'app/lib/angular/angular-debounce.min.js'                 
                 'app/lib/angular/angular-route.min.js'
                 'app/lib/angular/angular-sanitize.min.js'
                 'app/lib/angular/angular-touch.min.js'                 
                 'app/lib/angular-ui/angular-adaptive-templating.js'
                 'app/lib/angular-ui/angular-ui-router.min.js'
                 'app/lib/foundation/foundation-apps.min.js'
                 'app/lib/foundation/foundation-apps-templates.js'
                 'app/lib/matchMedia/matchmedia-ng.js'
                 'app/lib/hammer.min.js'
                 'app/lib/livestamp.min.js'
                 'app/lib/moment.min.js'
                 'app/js/**/*.js']                                                                                                                                                                                                                                                                               


    # watch <-- less / script watcher
    _watch =
        styles:
            files: ['app/css/less/*.less', 'app/css/**/*.css']
            tasks: ['build:dev']
            options: nospawn: true
        scripts:
            files: ['app/js/**/*.js']
            tasks: ['build:dev']
            options: nospawn: true
        partials:
            files: ['app/partials/**/*.*']
            tasks: ['build:dev']
            options: nospawn: true
        states:
            files: ['app/states/**/*.*']
            tasks: ['build:dev']
            options: nospawn: true
        index:
            files: ['app/index.html']
            tasks: ['build:dev']
            options: nospawn: true
            

    # build <-- runs the various build tasks for the specified environment
    _build = (targetEnv) -> 
        ### these are pre-build tasks are environment agnostic... ###
        
        ### these are build tasks that require an environment to be specified... ###
        
        if targetEnv is undefined
            targetEnv = "dev"
        
        
        # make sure target environment is supported...
        targetEnv = (targetEnv or "").toLowerCase()
        switch (targetEnv)
            when "dev", "qa", "beta", "prod", "phonegapbuild", "phonegapserve", "cordova", "" then break
            else throw grunt.util.error("Target Environment '#{targetEnv}' is either undefined or not supported")

        # clean / reset the target build folder
        grunt.task.run("clean:#{targetEnv}")
            
        # process less into CSS
        grunt.task.run("less")             
                
        
        # the build process for DEV
        if targetEnv is "dev" 
            #SOURCE FILES
            grunt.task.run("copy:fonts:web/dev")
            grunt.task.run("copy:images:web/dev")
            grunt.task.run("copy:partials:web/dev")                                                              
            grunt.task.run("copy:statesHtml:web/dev")            
            grunt.task.run("copy:web_config:web/dev")    
            
            #CSS FILES
            grunt.task.run("copy:css:web/dev")      
            
            #JS FILES
            grunt.task.run("copy:scripts:web/dev")                                         
            grunt.task.run("concat:js:web/dev")
            grunt.task.run("concat:statesJs:web/dev")                
            
            #PROCESS INDEX.HTML PAGE
            grunt.task.run("processhtml:dev:web/dev")  

                
        # the build process for PRODUCTION is different than other environments
        else if targetEnv is "prod"    
            #SOURCE FILES
            grunt.task.run("copy:fonts:web/prod")
            grunt.task.run("copy:images:web/prod")
            grunt.task.run("copy:partials:web/prod")                                                              
            grunt.task.run("copy:statesHtml:web/prod")            
            grunt.task.run("copy:web_config:web/prod")                       

            #CSS FILES
            grunt.task.run("concat:css:web/prod")            

            #JS FILES
            grunt.task.run("uglify:head:web/prod")            
            grunt.task.run("uglify:body:web/prod")
                                    
            #PROCESS INDEX.HTML PAGE
            grunt.task.run("processhtml:prod:web/prod")                        
                                         
                                                   
        # the build process for PHONEGAP BUILD
        else if targetEnv is "phonegapbuild"      
            #SOURCE FILES
            grunt.task.run("copy:fonts:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")
            grunt.task.run("copy:images:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")
            grunt.task.run("copy:partials:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")
            grunt.task.run("copy:statesHtml:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")            
            grunt.task.run("copy:app_root_phonegap_build_serve:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")	
            grunt.task.run("copy:res_phonegap:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")
                                        
            #CSS FILES
            grunt.task.run("concat:css:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")            
        
            #JS FILES
            grunt.task.run("uglify:head:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")            
            grunt.task.run("uglify:body:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")
        
            #PROCESS INDEX.HTML PAGE   
            grunt.task.run("processhtml:phonegap:mobile/phonegap-build/<%= projectVariables.phonegapBuild %>")             
            
                                    
        # the build process for PHONEGAP SERVE
        else if targetEnv is "phonegapserve"      
            #SOURCE FILES
            grunt.task.run("copy:fonts:mobile/phonegap-serve/www")
            grunt.task.run("copy:images:mobile/phonegap-serve/www")
            grunt.task.run("copy:partials:mobile/phonegap-serve/www")                                                              
            grunt.task.run("copy:statesHtml:mobile/phonegap-serve/www")            
            grunt.task.run("copy:app_root_phonegap_build_serve:mobile/phonegap-serve/www")	
            grunt.task.run("copy:res_phonegap:mobile/phonegap-serve/www")         
        
            #CSS FILES
            grunt.task.run("copy:css:mobile/phonegap-serve/www")            
                        
            #JS FILES
            grunt.task.run("copy:scripts:mobile/phonegap-serve/www")                                                                                 
            grunt.task.run("concat:js:mobile/phonegap-serve/www")
            grunt.task.run("concat:statesJs:mobile/phonegap-serve/www")                
                
            #PROCESS INDEX.HTML PAGE                
            grunt.task.run("processhtml:phonegapserve:mobile/phonegap-serve/www")    
            
                                    
        # the build process for PHONEGAP (MANUAL BUILDS VIA PHONEGAP CORDOVA CLI)   <%= projectVariables.cordovaName %>
        else if targetEnv is "cordova"      
            #SOURCE FILES
            grunt.task.run("copy:fonts:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")
            grunt.task.run("copy:images:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")
            grunt.task.run("copy:partials:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")
            grunt.task.run("copy:statesHtml:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")            
            grunt.task.run("copy:app_root_phonegap:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")	
            grunt.task.run("copy:res_phonegap:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")
            grunt.task.run("copy:phonegap_config:phonegap/cordova-cli/<%= projectVariables.cordovaName %>")	            
                        
            #CSS FILES           
            grunt.task.run("concat:css:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")            
                                                  
            #JS FILES         
            grunt.task.run("uglify:head:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")            
            grunt.task.run("uglify:body:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")

                        
            #PROCESS INDEX.HTML PAGE               
            grunt.task.run("processhtml:phonegap:phonegap/cordova-cli/<%= projectVariables.cordovaName %>/www")             
                        
        else                    
			# global_asax and web)config only appropriate for web installations (not necessary for phonegap)
            # for NON-PRODUCTION builds, just copy the necessary files to the target directory without modification...
        return        

    # watch <-- less / script watcher
    _cordovacli =
        options:
            path: '<%= paths.build %>/mobile/cordova-cli/<%= projectVariables.cordovaName %>'
            cli: 'cordova'
        cordova:            
            options:
                 command: ['create','platform','plugin']
                 platforms: ['ios','android']
                 plugins: ['device','dialogs','splashscreen','cordova-plugin-statusbar']
                 path: '<%= paths.build %>/mobile/cordova-cli/<%= projectVariables.cordovaName %>'
                 id: '<%= projectVariables.cordovaId %>'
                 name: '<%= projectVariables.cordovaName %>'


    # main grunt options
    _options =
        pkg: grunt.file.readJSON('package.json')
        clean: _clean
        concat: _concat
        copy: _copy
        imagemin: _imageMin
        less: _less        
        processhtml: _processHtml
#        robocopy: _robocopy        
        uglify: _uglify
        watch: _watch
        cordovacli: _cordovacli
        build: _build  
        projectVariables:
            phonegapBuild:  "Solutia-SeedApp-PhonegapBuild"
            cordovaId:		"com.seedapp"          
            cordovaName:	"Seed-App" 
        paths: 
            build:			"C:/ProjectStoreGit/Solutia-Seed-Angular-ZurbApp/Application/Client/SeedApp/_builds"

    grunt.initConfig(_options)
    
      
    # load plugins
    grunt.loadNpmTasks('grunt-cordovacli') 
    grunt.loadNpmTasks('grunt-contrib-clean')
    grunt.loadNpmTasks('grunt-contrib-concat')
    grunt.loadNpmTasks('grunt-contrib-copy')
    grunt.loadNpmTasks('grunt-contrib-less')
    grunt.loadNpmTasks('grunt-contrib-uglify')
    grunt.loadNpmTasks('grunt-contrib-watch')
    grunt.loadNpmTasks('grunt-processhtml')
    grunt.loadNpmTasks('grunt-shell')
    grunt.loadNpmTasks('grunt-text-replace')
    grunt.loadNpmTasks('grunt-robocopy')   
    
    # register tasks
    grunt.registerTask('build', 'Building application...', _build)
    grunt.registerTask('init-cordova', ['clean:cordovacli', 'cordovacli'])   
    return