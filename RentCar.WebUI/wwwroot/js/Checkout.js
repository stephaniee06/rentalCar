document.addEventListener("DOMContentLoaded", async function () {
    const urlParams = new URLSearchParams(window.location.search);
    const carIdParam = urlParams.get('carId');
    const pickupParam = urlParams.get('pickup');
    const returnParam = urlParams.get('return');

    if (carIdParam && document.getElementById("carName")) {
        try {
            const response = await fetch(`http://localhost:5000/api/Cars`);
            const cars = await response.json();
            const car = cars.find(c => c.carId == carIdParam);

            if (car) { 
                document.getElementById("carBrand").innerText = car.brand;
                const carFullName = `${car.brand} ${car.model} ${car.year}`;
                document.getElementById("carName").innerText = carFullName;
                
                 
                const transmisi = car.transmission || "Automatic";
                const kapasitas = car.capacity ? `${car.capacity} Penumpang` : "7 Penumpang";
              
                const options = { day: 'numeric', month: 'long', year: 'numeric' };
                const d1 = new Date(pickupParam);
                const d2 = new Date(returnParam);
                const tglSewaText = `${d1.toLocaleDateString('id-ID', options)} sampai ${d2.toLocaleDateString('id-ID', options)}`;
                
                document.getElementById("rentalDate").innerText = tglSewaText;
                document.getElementById("rentalPrice").innerText = `Rp. ${car.pricePerDay.toLocaleString('id-ID')} / hari`;
                
                const diffTime = Math.abs(d2 - d1);
                const totalHari = Math.ceil(diffTime / (1000 * 60 * 60 * 24)) || 1;
                const totalHarga = car.pricePerDay * totalHari;
                
                document.getElementById("totalPrice").innerText = `Rp. ${totalHarga.toLocaleString('id-ID')}`;

               
                let rawPath = car.msCarImages[0]?.image_link || car.msCarImages[0]?.imageLink || "";
                let fileName = rawPath.split('/').pop().split('\\').pop();
                document.getElementById("carImage").src = fileName ? `/cars/${fileName}` : '/cars/default.jpg';

               
                const btnSewa = document.getElementById("btnConfirmSewa");
                if (btnSewa) {
                    btnSewa.onclick = function () {
                      
                        const bookingData = {
                            tanggal: `${d1.toLocaleDateString('id-ID')} - ${d2.toLocaleDateString('id-ID')}`,
                            mobil: carFullName,
                            hargaPerHari: `Rp. ${car.pricePerDay.toLocaleString('id-ID')}`,
                            totalHari: totalHari,
                            totalHarga: `Rp. ${totalHarga.toLocaleString('id-ID')}`,
                            status: "Belum Dibayar"
                        };

                      
                        let listRiwayat = JSON.parse(localStorage.getItem("riwayatSewa")) || [];
                        listRiwayat.push(bookingData);
                        localStorage.setItem("riwayatSewa", JSON.stringify(listRiwayat));

                        alert("Pemesanan berhasil disimpan!");
                        window.location.href = "/Home/Riwayat";  
                    };
                }
            }
        } catch (err) {
            console.error("Error:", err);
        }
    }
});