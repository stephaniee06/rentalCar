 
const API_BASE_URL = "http://localhost:5000/api"; 

function formatRupiah(number) {
    return new Intl.NumberFormat('id-ID', {
        style: 'currency',
        currency: 'IDR',
        minimumFractionDigits: 0
    }).format(number);
}

function loadRentalHistory() {
    $('#rentalHistoryTable').DataTable({
        "processing": true,
        "serverSide": false, 
        "ajax": {
            "url": `${API_BASE_URL}/rental/history`, 
            "type": "GET",
            "datatype": "json",
            "beforeSend": function (xhr) {
               
                
            },
            "dataSrc": function (json) {
           
                return json; 
            }
        },
        "columns": [
            {  
                "data": null,
                "render": function (data, type, row) {
                    
                    const startDate = new Date(row.rentalDateStart).toLocaleDateString('id-ID');
                    const endDate = new Date(row.rentalDateEnd).toLocaleDateString('id-ID');
                    return `${startDate} - ${endDate}`;
                }
            },
            {  
                "data": null,
                "render": function (data, type, row) {
                    return `${row.carBrand} ${row.carModel} ${row.carYear}`;
                }
            },
            {  
                "data": "pricePerDay",
                "render": function (data, type, row) {
                    return formatRupiah(data);
                }
            },
            {  
                "data": "totalDays"
            },
            {  
                "data": "totalPrice",
                "render": function (data, type, row) {
                    return formatRupiah(data);
                }
            },
            { 
                "data": "paymentStatus",
                "render": function (data, type, row) {
                    let statusText = (data === 'Paid') ? 'Sudah Dibayar' : 'Belum Dibayar';
                    let statusClass = (data === 'Paid') ? 'status-paid' : 'status-unpaid';
                    return `<span class="${statusClass}">${statusText}</span>`;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.24/i18n/Indonesian.json" // Opsional: Bahasa Indonesia
        }
    });
}