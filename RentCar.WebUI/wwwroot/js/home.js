document.addEventListener("DOMContentLoaded", async function () {
    const btnCari = document.getElementById("btnCariMobil");
    const carContainer = document.getElementById("carListDisplay");

 
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
                document.getElementById("carName").innerText = `${car.brand} ${car.model}`;
                document.getElementById("rentalDate").innerText = `${pickupParam} s/d ${returnParam}`;
                document.getElementById("rentalPrice").innerText = `Rp ${car.pricePerDay.toLocaleString('id-ID')}`;
                
                const d1 = new Date(pickupParam);
                const d2 = new Date(returnParam);
                const days = Math.ceil((d2 - d1) / (1000 * 60 * 60 * 24)) || 1;
                document.getElementById("totalPrice").innerText = `Rp ${(car.pricePerDay * days).toLocaleString('id-ID')}`;

                
                let rawPath = car.msCarImages[0]?.image_link || car.msCarImages[0]?.imageLink || "";
                
                document.getElementById("carImage").src = fileName ? `/cars/${fileName}` : '/cars/default.jpg';
            }
        } catch (err) { console.error("Checkout Error:", err); }
    }

   
    if (carContainer) {
        carContainer.addEventListener("click", function (e) {
            if (e.target && e.target.classList.contains("btn-rent")) {
                const carId = e.target.getAttribute("data-id");
                const pickup = document.getElementById("pickupDate").value;
                const dataReturn = document.getElementById("returnDate").value;

                if (!pickup || !dataReturn) {
                    alert("Harap pilih tanggal pickup dan return!");
                    return;
                }
                window.location.href = `/Rental/Checkout?carId=${carId}&pickup=${pickup}&return=${dataReturn}`;
            }
        });
    }

 
    if (btnCari) {
        btnCari.addEventListener("click", async function (e) {
            e.preventDefault(); 
            const tahunRaw = document.getElementById("filterTahun").value;
            const tahunFilter = (tahunRaw === "Semua Tahun" || tahunRaw === "") ? null : parseInt(tahunRaw);

            carContainer.innerHTML = "<div class='no-data'>Mencari unit...</div>";

            try {
                const response = await fetch("http://localhost:5000/api/Cars/search", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        brand: "", 
                        keyword: "", 
                        year: tahunFilter,
                        minPrice: 0,
                        maxPrice: 10000000,
                        availabilityStatus: "Available"
                    })
                });

                const cars = await response.json();
                carContainer.innerHTML = "";

                if (!cars || cars.length === 0) {
                    carContainer.innerHTML = "<div class='no-data'>Armada tidak ditemukan.</div>";
                    return;
                }

                cars.forEach(car => {
                    let rawPath = "";
                    if (car.msCarImages && car.msCarImages.length > 0) {
                  
                        rawPath = car.msCarImages[0].image_link || car.msCarImages[0].imageLink || "";
                    }
                    
               
                    let fileName = rawPath.split('/').pop().split('\\').pop();
            
                    let imageSrc = fileName ? `/cars/${fileName}` : '/cars/default.jpg';

                    carContainer.innerHTML += `
                        <div class="car-card">
                            <img src="${imageSrc}" alt="${car.model}" 
                                 onerror="this.src='/cars/default.jpg'" 
                                 style="width:100%; height:180px; object-fit:cover;">
                            <div class="car-info">
                                <h3>${car.brand} ${car.model}</h3>
                                <p>Tahun: ${car.year}</p>
                                <p class="price">Rp ${car.pricePerDay.toLocaleString('id-ID')} / hari</p>
                                <button type="button" class="btn-rent" data-id="${car.carId}">Sewa Sekarang</button>
                            </div>
                        </div>`;
                });
            } catch (error) {
                console.error("Search Error:", error);
                carContainer.innerHTML = "<div class='no-data'>Koneksi API Gagal.</div>";
            }
        });
    }
});