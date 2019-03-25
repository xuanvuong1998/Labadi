var page = 1,
    inCallBack = false,
    isReachedScrollEnd = false;

var scrollHandler = function () {        
    if (isReachedScrollEnd == false &&
        $(document).scrollTop() <= $(document).height() - $(window).height()) {
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