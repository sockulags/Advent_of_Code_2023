# Advent of Code 2023

### Day 1: ✅
First and last digit part 1. String replace on part 2 , eg. one = o1e, etc. Needed to keep first and last letter to handle cases like eightwothree.
### Day 2: ✅
Part 1: Check if all conditions are met. If true, add ID to a score.
Part 2: Remember the highest number of each color, add the product of the numbers to score.
### Day 3: ✅
Part 1: Check surrondings for each number. If touching any symbol, add to score.
Part 2: Add every number that is in contact with a symbol. If a number touches multiple symbol add that number again with a diffrent key. Pair up gears based on keys. I made a hashkey and then compared the end of the key to get the pair for each symbol.
### Day 4: ✅
Part 1: Split winning numbers and ticker numbers and compare matches in arrays. Add the sum (2 raised to number of match - 1) to score.
Part 2: Same approach as part one. Added another int[] to keep track of how many copies each scratchcard got. If 4 numbers. Add 1 * n scratchcards to the next 4. n is the current number of scratchcards.
### Day 5: ✅
Part 1: Added a path class with start, source and range values. Made a list of type path for each step. Kept track of the lowest ending value.
Part 2: Wasn't sure how to approach this as this was my first time experiencing that my solution would take to long to finish. Came up with a halfing strategy where I searched my lowest starting value up to the highest starting value, and had to do a check if I got that value. I started with a range of 10^8 and then narrowed it down. When I got similar end result I just closed down the range I searched, and started over close to it. The solution took 2s in the end.
### Day 6: ✅
Part 1 and 2: Just a for loop. Traveled distance = time spent accelerating * (total time - time spent accelerating). If traveled distance broke the record, add to score.
### Day 7: ✅
Part 1: Parsed each card to a number. Sorted the string[] on the first char, then second char etc. Bet size * position = score. 
Part 2: Same but check all possible values for J and stored the hand with the highest value. 
### Day 8: ✅
### Day 9: ✅
### Day 10: ✅
### Day 11: ✅
### Day 12: ✅
### Day 13: ✅
### Day 14: ✅
### Day 15: ✅
### Day 16: ✅
### Day 17: ✅
### Day 18: ✅
### Day 19:
### Day 20:
### Day 21: ✅
### Day 22:
### Day 23:
### Day 24:
### Day 25:
