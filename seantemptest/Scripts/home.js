/*bs v3.3.7 jquery v3.3.1*/
var home = {
    data: {idx: 0},
    init: function () {
        $(document).on("click change", "button,input[type='text']", function () {
            $(".alert").removeClass("show");
        });
        $("#frm_dummy").on("submit", function (e) {
            e.preventDefault();
        });
        $("#btn_search").click(function () {
            if (!$("#frm_dummy")[0].checkValidity()) {
                $("#frm_dummy input[type='submit']").trigger("click");
                return false;
            }
            var qry = $("#txt_qry").val();
            home.showloading();

            $.ajax({
                url: searchUrl + "?query=" + qry,
                dataType: "json"
            }).done(function (data) {
                home.removeloading();
                if (data.success) {
                    var $pane = $("#dv_result");
                    $pane.html("");
                    var tmpl = $("#scp_well").html();
                    $.each(data.data.statuses, function () {
                        var html = tmpl.replace(/\{0\}/g, ++home.data.idx);
                        $pane.append(html);
                        var $curr = $pane.find(".card").last();
                        $("img", $curr).attr("src", this.Picture);
                        $("label[id^='lb_author_']", $curr).text(this.Author);
                        $("label[id^='lb_ts_']", $curr).text(this.TimeStamp);
                        $("p[id^='p_tw_']", $curr).text(this.Tweet);
                        $("label[id^='lb_tr_']", $curr).text(this.TotalRetweet);
                        $("label[id^='lb_tf_']", $curr).text(this.TotalFavorate);
                        $("a[id^='lnk_view_']", $curr).attr("href", this.Urls.length > 0 ? this.Urls[0].TweetUrl : "#");
                    });
                } else {
                    $("#lb_msg").text(data.msg);
                    $(".alert").addClass("show");
                }
                }).fail(function (xhr, status, error) { alert(xhr.responseText);});
        });
    },
    showloading: function () {
        var html = $("#scp_loading").html();
        $("body").append(html);
    },
    removeloading: function () {
        $("div.loading-overlay").remove();
    }
}