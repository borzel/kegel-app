if (typeof KegelApp == "undefined" || !KegelApp) {

    var KegelApp = {};
}

KegelApp.Controller = function(){
	
	var initLayout = function(){	
        var html = new EJS({
            url: "js/templates/layout.ejs"
        }).render({
        });
        $("#container").html(html);
	}
	
	initLayout();
}
