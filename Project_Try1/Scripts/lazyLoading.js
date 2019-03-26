var page = 1,
    inCallBack = false;

var scrollHandler = function () {
    //alert($(document).height() + " " + $(document).width() + " " + $(window).height() + " " + $(window).width());

    if ($(document).scrollTop() >= page * $(".quiz-home").height() * 0.86) {        
        loadQuizzes(url);
    }
}

function loadQuizzes(loadMoreRowsUrl) {
    if (page > 0 && !inCallBack) {
        inCallBack = true;
        page++;
        $.ajax({
            type: 'GET',
            url: loadMoreRowsUrl,
            data: "pageNum=" + page,
            success: function (res) {
                if (res != '') {
                    $(".quizList").append(res);
                } else {
                    page = 0;
                }
                inCallBack = false;
            }
        });
    }
}