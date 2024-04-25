using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Assignment4
{
	public class Client
	{
		private string _firstName = string.Empty;
		private string _lastName = string.Empty;
		private int _weight = 0;
		private int _height = 0;

  
        public Client(string firstName, string lastName, int weight, int height)
		{
			FirstName = firstName;
			LastName = lastName;
			Weight = weight;
			Height = height;
		}

		//Fully Implemented Properties
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException("First Name is required. Must not be empty or blank.");
				}
				_firstName = value;
			}
		}

		public string LastName
		{
			get { return _lastName; }
			set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException("Last Name is required. Must not be empty or blank.");
				}
				_lastName = value;
			}
		}

		public int Weight
		{
			get { return _weight; }
			set
			{
				if (value <= 0)
				{
					throw new ArgumentNullException("Weight must be a positive value (0 or greater).");
				}
				_weight = value;
			}
		}

		public int Height
		{
			get { return _height; }
			set
			{
				//if (int.IsNullOrWhiteSpace(value)) || (value <= 0)
				if (value <= 0)
				{
					throw new ArgumentNullException("Height must be a positive value (0 or greater).");
				}
				_height = value;
			}
		}

		//Read Only Fully Implemented Properties
		public double BmiScore
		{
			get
			{
				return GetBMIScore();
			}
		}

		public string BmiStatus
		{
			get
			{
				return GetBMIStatus();
			}
		}


		public string FullName
		{
			get
			{
				return $"{LastName}, {FirstName}";
			}
		}



		public double GetBMIScore()
		{
			double score = 0;
		
			score = Weight / Math.Pow(Height, 2) * 703;	

			return score;
		}

		public string GetBMIStatus()
		{
			string status = string.Empty;
			double clientWeight = GetBMIScore();

			switch (clientWeight)
			{
				case <= 18.4:
					status = "Underweight";

					break;
				case <= 24.9:
					status = "Normal";

					break;
				case <= 39.9:
					status = "Overweight";

					break;
				default:
					status = "Obese";

					break;
			}

			return status;

		}
		
			 
	}
}
