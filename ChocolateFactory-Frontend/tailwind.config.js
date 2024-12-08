/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,ts}'], // Correct content paths
  theme: {
    extend: {
      colors: {
        brown: {
          700: '#5D4037', // Comma here
          800: '#4E342E', // Comma here
          900: '#3E2723', // Comma here
        },
        yellow: {
          500: '#FFC107', // Comma here
          400: '#FFCA28', // Comma here
        },
        'choco': '#5C3C1D',
        'choco-500': '#6F4F23',
        'choco-600': '#4B2C19',
        'choco-100': '#D9A98D',
        'brown-600': '#4B2C19',

      },
    },
  },
  plugins: [],
};


