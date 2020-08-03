<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
</Query>

/*
Basic example of DataAnotations
IValidatableObject.Validate allows for validation outside of the contract annotations
*/

void Main()
{
	var x = new StayDates()
	{
		MarketId = -1,
		MarketSegmentId = -1,
		SegmentUnitId = -1,
		UnitId = -1,
		SegmentDates = new List<UserQuery.SegmentDates>()
		{
			new SegmentDates() {
				DateType = 1,
				StartDate = DateTime.Parse("1/1/2016"),
				EndDate = DateTime.Parse("1/11/2016")
			},
			new SegmentDates() {
				DateType = 2,
				StartDate = DateTime.Parse("11/11/2016"),
				EndDate = DateTime.Parse("1/11/2016")
			},
			new SegmentDates() {
				DateType = 1,
				StartDate = DateTime.Parse("11/11/2016")
			},
				new SegmentDates() {
				DateType = -1,
				StartDate = DateTime.Parse("1/11/2016"),
                EndDate = DateTime.Parse("1/15/2016")
			}


		}
	};

	var context = new ValidationContext(x);
	var results = new List<ValidationResult>();
	Validator.TryValidateObject(x, context, results, true);  //Validates Fields
	Validator.TryValidateObject(x, context, results, false); //Validates IValidateObject
	results.Dump("Validation Errors");
}


public class StayDates : IValidatableObject
{
	public StayDates()
	{
		SegmentDates = new List<SegmentDates>();
	}

	[Required(ErrorMessage = "The {0} is required.")]
	[Range(1, Int32.MaxValue, ErrorMessage = "The {0} must be a valid Id.")]
	public int MarketId { get; set; }

	[Required(ErrorMessage = "The {0} is required.")]
	[Range(1, Int32.MaxValue, ErrorMessage = "The {0} must be a valid Id.")]
	public int MarketSegmentId { get; set; }

	public int? SegmentUnitId { get; set; }

	[Required(ErrorMessage = "The {0} is required.")]
	[Range(1, Int32.MaxValue, ErrorMessage = "The {0} must be a valid Id.")]
	public int UnitId { get; set; }

	public List<SegmentDates> SegmentDates { get; set; }

	public List<string> ErrorMessages { get; set; }

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{

		var results = new List<ValidationResult>();
		if (SegmentUnitId != null && SegmentUnitId.HasValue & SegmentUnitId.Value <= 0)
		{
			results.Add(new ValidationResult("When updating a Segment Unit, the SegmentUnitId must be a valid Id.", new string[] { "SegmentUnitId" }));
		}

		var duplicateDateRanges = SegmentDates.GroupBy(g => new { g.StartDate, g.EndDate }).Where(w => w.Count() > 1).Select(s => s.ToList());
		if (duplicateDateRanges != null && duplicateDateRanges.Any())
		{
			results.Add(new ValidationResult(
				"Could not create a new Channel Inventory Segment record. Duplicate date ranges are not allowed.", new string[] {"SegmentDates"}));

			foreach (var duplicateList in duplicateDateRanges)
			{
					duplicateList.Select(dupe =>
										  string.Format("Duplicate: {0}:{1}-{2}",
											  dupe.DateType,
											  dupe.StartDate.ToString("d"),
											  dupe.EndDate.ToString("d"))).ToList().ForEach(s => results.Add(new ValidationResult(s)));
											  ;
			}
		}

		this.SegmentDates.ForEach(sd =>
			{
				var context = new ValidationContext(sd, validationContext.ServiceContainer, validationContext.Items);
				Validator.TryValidateObject(sd, context, results, true); //Validates IValidatableObject
				Validator.TryValidateObject(sd, context, results, false); //Validates Fields
			}
		);

		return results;
	}
}

public class SegmentDates : IValidatableObject
{
	[Required(ErrorMessage = "The {0} is required.")]
	[Range(1, Int32.MaxValue, ErrorMessage = "The {0} must be a valid Id.")]
	public int DateType { get; set; }

	[Required(ErrorMessage = "The {0} is required.")]
	[Range(typeof(DateTime),"1/1/1901","12/31/9999", ErrorMessage = "The {0} is required.")]
	public DateTime StartDate { get; set; }

	[Required(ErrorMessage = "The {0} is required.")]
	[Range(typeof(DateTime), "1/1/1901", "12/31/9999", ErrorMessage = "The {0} is required.")]
	public DateTime EndDate { get; set; }

	public List<string> ErrorMessages { get; set; }

	public SegmentDates()
	{
		ErrorMessages = new List<string>();
	}

	public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	{
		if (StartDate >= EndDate)
		{
			yield return new ValidationResult(string.Format("The StartDate ({0}) must be before the specified EndDate ({1}).", StartDate.ToString("d"), EndDate.ToString("d")), new List<string>() { "StartDate", "EndDate" });
		}
	}
}