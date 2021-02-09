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
