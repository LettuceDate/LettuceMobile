using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using Lettuce.Core;

namespace Lettuce.IOS
{
	public class DateCollectionSource : UICollectionViewDataSource
	{
		private List<BaseDate> dateList { get; set; }
		public int collectionType { get; set;}

		public DateCollectionSource()
		{
		}

		public void SetData(List<BaseDate> theList)
		{
			dateList = theList;
		}

		public override nint GetItemsCount(UICollectionView collectionView, nint section)
		{
			if ((dateList == null) || (dateList.Count == 0))
				return (nint)0;
			else
				return (nint)dateList.Count;
		}


		public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
		{
			DateSummaryCollectionViewCell cell = collectionView.DequeueReusableCell(DateSummaryCollectionViewCell.Key, indexPath) as DateSummaryCollectionViewCell;

			if ((dateList == null) || (dateList.Count == 0))
				cell.ConformToEmpty();
			else
				cell.ConformToRecord(dateList[indexPath.Row], collectionType);

			return cell;
		}
	}
}

