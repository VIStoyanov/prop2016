var deadline = new Date("July 16, 2016 18:00:00");
// var deadline = new Date("June 26, 2016 13:53:30");

function getTimeRemaining(endtime){
	var t = Date.parse(endtime) - Date.parse(new Date());
	var seconds = Math.floor( (t/1000) % 60 );
	console.log(endtime);
	var minutes = Math.floor( (t/1000/60) % 60 );
	var hours = Math.floor( (t/(1000*60*60)) % 24 );
	var days = Math.floor( t/(1000*60*60*24) );
	return {
		'total': t,
		'days': days,
		'hours': hours,
		'minutes': minutes,
		'seconds': seconds
	};
}

function initializeClock(endtime){
	var timeinterval = setInterval(function() {
		var t = getTimeRemaining(endtime);
		console.log(t);
		$('#display-clock').html('Days: <p>' + t.days + '</p> Hours: <p>'+ t.hours + '</p> Minutes: <p>'
								 + ('0' + t.minutes).slice(-2) + '</p> Seconds: <p>' + ('0' + t.seconds).slice(-2) + '</p>');

		if(t.total<=0){
			clearInterval(timeinterval);
			$('#display-clock').html('Enjoy the event!');
			$('#clock').find('h2').remove();
		}
	},1000);
}

$(document).ready(initializeClock(deadline));