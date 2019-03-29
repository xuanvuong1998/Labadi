var status = "waiting";
var mainHub = $.connection.mainHub;

function showStatus() {
    $("#waitingForAns").css(
        {
            'display': 'none'
        }
    );

    $("#main-question").css(
        {
            'display': 'none'
        }
    )

    $("#status").css(
        {
            'display': 'block'
        }
    );

    
}


function waitForAns() {
    $("#main-question").css(
        {
            'display': 'none'
        }
    )
    $("#waitingForAns").css(
        {
            'display': 'inline-block',
        }
    )
}

function showQuestion() {
    $("#status").css(
        {
            'display': 'none'
        }
    );
    $("#ready").css(
        {
            'display': 'none'
        }
    );
    $("#main-question").css(
        {
            'display': 'inline-block'
        }
    )
}

function checkResult(selectedAnswer) {

    status = "selected";
    var ansContent = selectedAnswer.innerHTML;

    $("#selectedAns").html(ansContent);

    waitForAns();
}

function showSummary() {
    $("#status").css(
        {
            'display': 'none'
        }
    );
    $("#main-question").css(
        {
            'display': 'none'
        }
    )
    $("#waitingForAns").css(
        {
            'display': 'none',
        }
    )

    $("#summary").css(
        {
            'display': 'block'
        }
    );


}

$(function () {
    mainHub.client.sendSummary = function (summary) {
        var list = summary.split(';');

        list.forEach(function (item) {
            $("#summary").append("<div class='button-format div-result'>" + htmlEncode(item) + "</div><br/>");
        });

        //$("#summary").html(summary);
        showSummary();
    }

    mainHub.client.receiveQuestion = function (content, ans1, ans2, ans3, ans4, ans, image, time) {
        if (status != "waiting") {       
            return;
        }        
        status = "thinking"; 
        showQuestion();
        $("#image").attr('src', image);
        $("#content").html(content);
        $("#ans1").html(ans1);
        $("#ans2").html(ans2);
        $("#ans3").html(ans3);
        $("#ans4").html(ans4);
        $("#timer-counter").html(time);

        activeTimer(ans);
    };

    $.connection.hub.start().done(function () {
        mainHub.server.acceptPlayers();
    });
});


function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}


function isCorrect(selectedAns, rightAns) {

    var ans;
    if (rightAns == "A") {
        ans = $("#ans1").html();
    }
    if (rightAns == "B") {
        ans = $("#ans2").html();
    }
    if (rightAns == "C") {
        ans = $("#ans3").html();
    }
    if (rightAns == "D") {
        ans = $("#ans4").html();
    }

    return ans == selectedAns;
}

function activeTimer(rightAns) {
    var z = document.getElementById("timer-counter").innerText;
   
    var y = parseInt(z);
    var x = setInterval(function () {
                
        y--;
        document.getElementById("timer-counter").innerText = y + "";

        if (y <= 0) {
            
            clearInterval(x);
            document.getElementById("timer-counter").innerText = "Time up ! ";

            var result = true;
            if (status == "thinking") {
                result = false;
                $("#status").html("You did not choose any answer!!!");
                $("#status").css(
                    {
                        'background-image': 'linear-gradient(to bottom right,blue, darkturquoise)'
                    });
            }else if (isCorrect($("#selectedAns").html(), rightAns)) {
                $("#status").html("CORRECT!!!");

                $("#status").css(
                    {
                        'background-image': 'linear-gradient(to bottom right,green, greenyellow)'
                    });
            } else {
                result = false;
                $("#status").html("INCORRECT!!!");
                $("#status").css(
                    {
                        'background-image': 'linear-gradient(to bottom right,crimson, orangered)'
                    });
            }

            status = "waiting";
            showStatus();
            mainHub.server.sendReport(result);
        }
    }, 1000);
}

