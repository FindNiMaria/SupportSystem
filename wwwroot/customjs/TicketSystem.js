$(document).on("change", " #CategoryId.get-subcategories", function (e) {
    var working = "<span id = 'spin' ><'i class 'fa fa-spin fa-3x fa-fw'></span>
    if ($(this).val() !== "") {
        $.ajax(
        {
            url: "/Data/GetTicketSubCategories/" + $(this).val(),
            dataType: "json",
            crossDomain: true,

                beforeSend: function ()
                {
                    $(this).parent().append(working);
                    $("#SubCategoryId").html("");
                    $("#SubCategoryId").append("<option value> --Select Sub-Categories -- </option>");

                },

                sucess: function (json)
                {
                    var data = json
                    console.log(data);

                    $(data).map(function ()
                    {
                        $("SubCategoryId").append($('<option></option>').val(this.id).html(this.name));
                    });
                },

                error: function (xhr, ajaxOptions, trownError)
                {
                    
                },
                complete: function ()
                {
                    $("#spin").remove();
                }
        });

