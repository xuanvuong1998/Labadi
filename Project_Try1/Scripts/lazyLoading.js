var page = 1,
    inCallBack = false,
    isReachedScrollEnd = false;

var scrollHandler = function () {
    loadQuizzes(url);
    if (isReachedScrollEnd == false &&
        $(document).scrollTop <= $(document).height - $(window).height) {
       
    }
}

function loadQuizzes(loadMoreRowsUrl) {
    if (page > 0 && !inCallBack) {
        inCallBack = true;
        page++;
        $.ajax({
            type: 'GET',
            url = loadMoreRowsUrl,
            data = "pageNum=" + page,
            success: function (data, textStatus) {
                if (data != '') {
                    $("quizList").append(data);
                } else {
                    page = 0;
                }

                inCallBack = false;                
            }
        });
    }
}