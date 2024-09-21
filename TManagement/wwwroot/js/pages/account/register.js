
var cities = null;
$(function () {

   
    $.getJSON(`/api/loockups/getall/5`, function (data) {
        cities = data.result;


        //  check  hdn field --> value
        // fillcities 
        //  set  selected city

       
    });

    var country = $('#cmbCountry');

    country.on('change', function () {

        //alert(country.val());

        if (country.val() != '') {
           

         //   alert(JSON.stringify(cities));
            var data = cities.filter(function (value, index) {

              //  alert(JSON.stringify(value));

                return value.fatherLookupId==country.val()
            });

                var result = $.map(data, function (val, i) {
                    return `<option value='${val.id}'>${val.name}</option>`;
                });



                $('#cmbCity').html(['<option value"">--select--</option>', ...result].join(''));
            

        }
    });

});