document.addEventListener("DOMContentLoaded", function () {
    const timers = document.querySelectorAll(".auction-timer");

    timers.forEach(timer => {
        const startTime = new Date(timer.getAttribute("data-start-time"));

        function updateTimer() {
            const now = new Date();
            const timeDiff = startTime - now;

            if (timeDiff <= 0) {
                timer.innerHTML = "Auction has started!";
                return;
            }

            const days = Math.floor(timeDiff / (1000 * 60 * 60 * 24));
            const hours = Math.floor((timeDiff % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            const minutes = Math.floor((timeDiff % (1000 * 60 * 60)) / (1000 * 60));
            const seconds = Math.floor((timeDiff % (1000 * 60)) / 1000);

            timer.querySelector("span").textContent = `${days}d ${hours}h ${minutes}m ${seconds}s`;
        }

        // Initial update
        updateTimer();

        // Update every second
        setInterval(updateTimer, 1000);

        console.log("Timers found:", timers);
        console.log("Start time for timer:", startTime);


    });
});
