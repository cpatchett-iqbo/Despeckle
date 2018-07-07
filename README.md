# Despeckle: Image Noise Removal</br>
### Original [code](https://github.com/Mohamed-Ali-Mohamed/IMAGE-FILTERS) and [documentation](https://progmohamedali.wordpress.com/2014/02/24/image-filters-noise-removal-in-image-processing/) by [Mohamed Ali](https://progmohamedali.wordpress.com/about/). </br>Changes by Craig Patchett of [IQ BackOffice Inc.](http://www.iqbackoffice.com)
---
### Order Statistics Filters

In image processing, filters are usually necessary to perform a high degree of noise reduction in an image before performing higher-level processing steps. The order statistics filter is a non-linear digital filter technique, often used to remove [speckle](http://en.wikipedia.org/wiki/Speckle_noise) ([salt and pepper](http://en.wikipedia.org/wiki/Salt_and_pepper_noise)) [noise](http://en.wikipedia.org/wiki/Signal_noise) from images. We target two common filters in this project:

1. Alpha-trim filter
2. Adaptive median filter

The main idea of both filters is to sort the pixel values in a neighborhood region with certain window size and then chose/calculate the single value from them and places it in the center of the window in a new image, see figure 1. This process is repeated for all pixels in the original image.

![Figure 1: Main idea of order-statistics filters](https://progmohamedali.files.wordpress.com/2014/02/image-filters-1.png)

Figure 1: Main idea of order-statistics filters

As the window size increases, the effect of the filter is increased, as shown in figure 3.

### Alpha-Trim Filter

The idea is to calculate the average of some neighboring pixels’ values after trimming out (excluding) the smallest T pixels and largest T pixels. This can be done by repeating the following steps for each pixel in the image:

1. Store the values of the neighboring pixels in an array. The array is called the window, and it should be odd sized.
2. Sort the values in the window in ascending order.
3. Exclude the first T values (smallest) and the last T values (largest) from the array.
4. Calculate the average of the remaining values as the new pixel value and place it in the center of the window in the new image, see figure 1.

This filter is usually used to remove both salt & pepper noise and random noise. See figure 2.

#### Notes

* We work on gray-level images. So, each pixel has a value ranged from 0 to 255, where 0 is black and 255 is white.

![Figure 2: Effect of the alpha-trim filter with window size = 5x5 and trim value T = 5](https://progmohamedali.files.wordpress.com/2014/02/image-filters2.png?w=620&h=312)

Figure 2: Effect of the alpha-trim filter with window size = 5x5 and trim value T = 5

![Figure 3: Effect of the standard median filter with different window sizes](https://progmohamedali.files.wordpress.com/2014/02/image-filters3.png?w=620&h=446)

Figure 3: Effect of the standard median filter with different window sizes

### Adaptive Median Filter

The idea of the **standard median filter** is similar to that of the alpha-trim filter, but instead we calculate the median of neighboring pixels’ values (middle value in the window array after sorting) instead of the average.

It’s usually used to remove the salt and pepper noise, see figure 3.

However, the standard median filter has the following drawbacks:

1. It fails to remove salt and pepper noise with large percentage (greater than 20%) without causing distortion in the original image.
2. It usually has a side-effect on the original image, especially when it’s applied with a large mask size (see figure 2 with window 7×7).

The **Adaptive median filter** is designed to handle these drawbacks by:

1. Seeking a median value that’s not salt or pepper noise by increasing the window size until reaching such median.
2. Replacing the noise pixels only (i.e., if the pixel is not a salt or a pepper, then leave it).

This is clear in figure 4 and figure 5. Compare the effect of both filters in each case. Note that both can remove the noise, but the adaptive median filter doesn't cause large distortions of the original image like the standard filter does.

![Figure 4: Effect of adaptive vs. standard median filter on small percentage of salt and pepper noise](https://progmohamedali.files.wordpress.com/2014/02/image-filters4.png?w=620&h=370)

Figure 4: Effect of adaptive vs. standard median filter on small percentage of salt and pepper noise

![Figure 5 Effect of adaptive vs. standard median filter on large percentage of salt and pepper noise](https://progmohamedali.files.wordpress.com/2014/02/image-filters5.png?w=620&h=481)

Figure 5 Effect of adaptive vs. standard median filter on large percentage of salt and pepper noise

### Steps to Apply an Adaptive Median Filter to an Image

The adaptive median filter has a variable window size **W<sub>S</sub>**, and the procedure of updating the pixel value is as follows:

#### For each pixel in the image:

Try window sizes ranging from 3x3 to W<sub>S</sub> x W<sub>S</sub>, where W<sub>S</sub> is the maximum window size entered by the user, as follows:

#### Step 1: Start with window size 3x3

#### Step 2: Choose a non-noise median value

Sort the current window, and denote the following:

1. Z<sub>xy</sub> is the gray value of the current pixel value at location (x, y).
2. Z<sub>max</sub> is the maximum gray value in the window.
3. Z<sub>min</sub> is the minimum gray value in the window.
4. Z<sub>med</sub> is the median gray value in the window.
 
A1 = Z<sub>med</sub> - Z<sub>min</sub>  
A2 = Z<sub>max</sub> - S<sub>med</sub>

If A1 > 0 and A2 > 0 then we have found a non-noise median, in which case go to step 3. 

Otherwise, increase the window size by 2. If the new window size <= W<sub>S</sub> then start at step 2 again with the new window size.

If the new window size > W<sub>S</sub> then:

NewPixelVal = Z<sub>med</sub>

#### Step 3: Replace the center with the median value, or leave it as is

B1 = Z<sub>xy</sub> = Z<sub>min</sub>  
B2 = Z<sub>max</sub> - Z<sub>xy</sub>

If B1 > 0 and B2 > 0 then leave the center pixel as it is:

NewPizelVal = Z<sub>xy</sub>

Otherwise, replace the center pixel with the median value:

NewPixelVal = Z<sub>med</sub>

#### Step 4: Repeat the process for the next pixel starting from step 1 again

### Huh?!

The steps in the previous section summarize what's done through an adaptive median filter implementation from a programming standpoint. From an application standpoint, here's what's going on.

#### Step 1: Focus on the details

We start with a small window in order to address smaller noise first.

#### Step 2: Search for a true median

If the current window has a true median (the median value is different from both the minimum and maximum values) then we can skip to the next step. If not, then we want to try the next larger window size so we set the window to that size and check again. We keep doing this until we find a true median or we reach the maximum window size. In the case of the latter, we set the output pixel value to the median value, move on to the next pixel, and go back to step 1.

#### Step 3: We have a true median

If the value of the center pixel in the window is different from the minimum and maximum values (i.e., it's not noise) then we set the output pixel to the current pixel value (i.e., no change). If the value of the center pixel is equal to either the minimum or maximum value, then we set the output pixel value to the median value. Either way, we then move on to the next pixel and go back to step 1.