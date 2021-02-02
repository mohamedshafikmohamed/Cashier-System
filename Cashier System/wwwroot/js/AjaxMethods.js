function Delete_bill(x) {
    $.ajax(
        {
            type: "POST",
            url: "/bills/Delete_bill",
            data: { Id: x },
            datatype: "json",

        }

    )

};

function a(x) {
    $.ajax(
        {
            type: "POST",
            url: "/bills/Addproducttobill",
            data: { code: x },
            datatype: "json",

        }

    )
    $("#div1").load("/Bills/GetPartial");
};