$("#SuccessRetry").click(function() {
	var container = $('#container');
	$.get("/success", function(data) {
		container.html(data);
	});
});

$("#FailureRetry").click(function() {
	var container = $('#container');
	$.get("/failure", function(data) {
		container.html(data);
	});
});