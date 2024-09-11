

$(function () {


    var country = $('#cmbCountry');

    country.on('change', function () {

        //alert(country.val());

        if (country.val() != '') {
            $.getJSON(`/account/getcities?countryId=${country.val()}`, function (data) {
                //JSON.parse("{id:1 , name:'atallah'}")

                var result = $.map(data, function (val, i) {
                    return `<option value='${val.id}'>${val.name}</option>`;
                });



                $('#cmbCity').html(['<option value"">--select--</option>', ...result].join(''));
            });

        }
    });

});