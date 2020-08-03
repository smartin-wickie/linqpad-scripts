<Query Kind="Program">
  <NuGetReference>NUnitLite</NuGetReference>
  <Namespace>NUnit.Framework</Namespace>
</Query>

void Main()
{
	/*
		https://www.algoexpert.io/questions/Max%20Profit%20With%20K%20Transactions
		
		You are given an array of integers representing the prices of a single stock on various days (each index in the array represents a different day). 
		You are also given an integer k, which represents the number of transactions you are allowed to make. One transaction consists of buying the stock 
		on a given day and selling it on another, later day. Write a function that returns the maximum profit that you can make buying and selling the stock,
		given k transactions. Note that you can only hold 1 share of the stock at a time; in other words, you cannot buy more than 1 share of the stock on 
		any given day, and you cannot buy a share of the stock if you are still holding another share. Note that you also don't need to use all k 
		transactions that you're allowed.

		

	*/



	MaxProfitWithKTransactions(
	new int[]{5,11,3,50,40,90}
	, 2
	).Dump();
}

public static int MaxProfitWithKTransactions(int[] prices, int k)
{
	if (prices.Length < 2 || k < 1)	
		return 0;
	
	var transactionSetMax = k+1;
	var daysMax = prices.Length;
	var profitTable = new int[transactionSetMax,daysMax]; 
	var priorTransactionSet = new int[transactionSetMax,daysMax];
	
	for (int r = 1; r < transactionSetMax; r++)
	{
		for (int c = 1; c < daysMax; c++)
		{
			var priorDayProfit = profitTable[r, c - 1];
			var currentDayPrice = prices[c];
			var bestPriorTransactionSetProfit =
				currentDayPrice +
				Enumerable.Range(0, c )
				.Select(day => -prices[day] + profitTable[r - 1, day])
				.Max();

			profitTable[r, c] = (new int[] {priorDayProfit,bestPriorTransactionSetProfit}).Max() ;
			
		}
	}
		
	//profitTable.Dump();
	return profitTable[transactionSetMax-1,daysMax-1];
}

	// Define other methods, classes and namespaces here
public static int MaxProfitWithKTransactions_Bad(int[] prices, int k)
{
	prices.Length.Dump();
	var periods = new List<potentialTransactionPeriod>();
	var rangeIdxs = Enumerable.Range(0, prices.Length);

	var ranges = rangeIdxs
		.SelectMany(a => rangeIdxs.Where(w => w > a)
			.Select(b => new { a, b }))
			.Select(period => new potentialTransactionPeriod()
			{
				startIndex = period.a,
				endIndex = period.b,
				transactionProfit = prices[period.b] - prices[period.a]
			});

	var profitableRanges = ranges.Where(w => w.transactionProfit > 0).ToList();
	if (!profitableRanges.Any())
		return 0;

	if (k == 1)
		return ranges.Max(r => r.transactionProfit);

	Func<potentialTransactionPeriod, potentialTransactionPeriod, bool> intersect 
		= (periodA, periodB) => { 
			return Enumerable.Range(periodA.startIndex,periodA.endIndex-periodA.startIndex)
					.Intersect(Enumerable.Range(periodA.startIndex,periodA.endIndex-periodA.startIndex))
					.Any();
			};

	var candidateTransactions = new List<int>();
	profitableRanges.ForEach(pr =>
	{
		var compatibleRanges = profitableRanges.Where(
			cr => !intersect(pr,cr)			
			);
		
	});	
	
								
								
	ranges.Where(w => w.transactionProfit > 0).OrderByDescending(r => r.transactionProfit ) .Dump();								
	return ranges.Max(r => r.transactionProfit );


}


private class potentialTransactionPeriod
{
	public int startIndex { get; set; }
	public int endIndex { get; set; }
	public int transactionProfit { get; set; }
}