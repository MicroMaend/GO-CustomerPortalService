let currentSlide = 0;

function moveSlide(direction) {
    const slider = document.querySelector('.slider');
    const slides = document.querySelectorAll('.slide');
    const totalSlides = slides.length;

    // Opdater den aktuelle slide baseret på retning
    currentSlide = (currentSlide + direction + totalSlides) % totalSlides;
    slider.style.transform = `translateX(-${currentSlide * 100}%)`;

    // Opdater prikkerne
    updateDots();
}

function goToSlide(slideIndex) {
    const slider = document.querySelector('.slider');
    currentSlide = slideIndex;
    slider.style.transform = `translateX(-${currentSlide * 100}%)`;

    // Opdater prikkerne
    updateDots();
}

function updateDots() {
    const dots = document.querySelectorAll('.slider-dots .dot');
    dots.forEach((dot, index) => {
        dot.classList.toggle('active', index === currentSlide);
    });
}

// Event listeners for prikkerne
document.addEventListener('DOMContentLoaded', () => {
    const dots = document.querySelectorAll('.slider-dots .dot');
    dots.forEach((dot, index) => {
        dot.addEventListener('click', () => goToSlide(index));
    });
});


