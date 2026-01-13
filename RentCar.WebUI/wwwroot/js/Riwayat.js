document.addEventListener("DOMContentLoaded", function () {
    const tableBody = document.getElementById("tableBodyRiwayat");
    
 
    const listSewa = JSON.parse(localStorage.getItem("riwayatSewa")) || [];

    if (listSewa.length === 0) {
        tableBody.innerHTML = `<tr><td colspan="6" style="text-align:center;">Belum ada riwayat.</td></tr>`;
        return;
    }

 
    tableBody.innerHTML = listSewa.map(item => `
        <tr>
            <td>${item.tanggal}</td>
            <td>${item.mobil}</td>
            <td>${item.hargaPerHari}</td>
            <td>${item.totalHari}</td>
            <td>${item.totalHarga}</td>
            <td class="${item.status === 'Sudah Dibayar' ? 'status-lunas' : 'status-pending'}">
                ${item.status}
            </td>
        </tr>
    `).join('');
});