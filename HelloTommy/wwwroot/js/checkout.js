//Get reference to items
//Arrays
const productButtons = document.querySelectorAll('.productDetails-btn');
const productItems = document.querySelectorAll('.product-item');

const detailsContainer = document.querySelectorAll('.checkout-details-container');

//Loop through arrays, to give each incrementing name 
//Product-item
for (let i = 0; i < productItems.length; i++) {
    productItems[i].id = `p${i}`;
}
//Expand-button
for (let i = 0; i < productButtons.length; i++) {
    productButtons[i].id = `b${i}`;
}
//Details-container    
for (let i = 0; i < detailsContainer.length; i++) {
    detailsContainer[i].id = `d${i}`;
}

//For each button... 
productButtons.forEach((x) => {
    //Wait for an input
    x.addEventListener('click', () => {
        //and Loop through the products
        for (let productItem of productItems) {
            const buttonId = x.id[1];
            const productId = productItem.id[1];

            //Compare the id, product with button, to find the corresponding product 
            if (productId == buttonId) {
                //Add contra remove the detail-container
                if (detailsContainer[buttonId].classList.contains('hidden')) {
                    detailsContainer[buttonId].classList.remove('hidden');
                }
                else {
                    detailsContainer[buttonId].classList.add('hidden');
                }
            }
        }
    })
})