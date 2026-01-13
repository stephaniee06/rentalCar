let allCars = [];  

async function fetchCars() {
    try {
        const res = await fetch("http://localhost:5000/api/Cars");  
        if (!res.ok) throw new Error("Gagal ambil data");
        
        allCars = await res.json();
     
    } catch (err) {
        console.error("Error:", err);
    }
}

function displayCars(data) {
    const carList = document.getElementById("carList");
    carList.innerHTML = "";
    
    data.forEach(car => {
        carList.innerHTML += `
        <div class="car-card">
            <img src="/images/${car.status}" alt="${car.brand}"> 
            <h2>${car.brand} ${car.model} (${car.year})</h2>
            <p>Harga: Rp. ${car.pricePerDay.toLocaleString()} / hari</p>
            <button onclick="location.href='/Rental/Summary?id=${car.carId}'">Sewa Sekarang</button>
        </div>
        `;
    });  
}

 
document.getElementById("searchBtn").addEventListener("click", () => {
    let filtered = allCars;
    const yearVal = document.getElementById("yearFilter").value;

    if (yearVal !== "all") {
        filtered = allCars.filter(c => c.year == yearVal);
    }

    document.getElementById("sortingSection").classList.remove("hidden");
    document.getElementById("carList").classList.remove("hidden");
    displayCars(filtered);
});

 
document.getElementById("sortOption").addEventListener("change", (e) => {
    let sorted = [...allCars];
    if (e.target.value === "low-high") {
        sorted.sort((a, b) => a.pricePerDay - b.pricePerDay);
    } else {
        sorted.sort((a, b) => b.pricePerDay - a.pricePerDay);
    }
    displayCars(sorted);
});
 
document.addEventListener("DOMContentLoaded", fetchCars);