var PhoneGapInit = function ()
{
	this.boot = function ()
	{
		//alert('DAS BOOT');
		angular.bootstrap(document, ['seedApp']);

	};

	if (window.cordova !== undefined)
	{
		//alert('PhoneGapInit - WITH PHONEGAP');
		//console.log('PhoneGap  found, booting Angular manually');
		this.boot();
	}
	else
	{
		//alert('PhoneGapInit - WITHOUT PHONEGAP');
		//console.log('PhoneGap NOT found, booting Angular manually');
		this.boot();
	}
};

var app =
{
	initialize: function ()
	{
		this.bindEvents();
	},

	bindEvents: function ()
	{
		if (window.cordova !== undefined)
		{
			document.addEventListener('deviceready', this.onDeviceReady, true);
		}
		else
		{
			angular.element(document).ready(function ()
			{
				new PhoneGapInit();
			});
		}
	},

	onDeviceReady: function ()
	{
		if (window.device)
		{
			//alert('PHONEGAP IS READY - version: ' + window.device.version);

			if (parseFloat(window.device.version) >= 7)
			{
				//document.body.style.marginTop = "20px";
				StatusBar.overlaysWebView(false);
				StatusBar.backgroundColorByHexString('#162333');
			}
		}

		angular.element(document).ready(function ()
		{
			new PhoneGapInit();
		});

		document.addEventListener('online', this.onOnLine, true);
		document.addEventListener('offline', this.onOffLine, true);
	},

	onOnline: function ()
	{
		alert('welcome to online mode');
	},

	onOffLine: function ()
	{
		alert('oops. connection lost');
	},

	receivedEvent: function (id)
	{
		alert('received event: ' + id);
	}
};