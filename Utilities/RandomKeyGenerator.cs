using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Aplication.Utilities
{
	public class RandomKeyGenerator
	{
		private static Random _random = new Random();

		/// <summary>
		/// Generate lemgthy random values (string)
		/// </summary>
		/// <returns></returns>
		public static string GenerateGuId()
		{


			// Generate new Guid.
			string randomNumberWithoutDash = Guid.NewGuid().ToString().Replace("-", "");

			return randomNumberWithoutDash;
		}
		/// <summary>
		///  Generates a random string with a given size.
		/// </summary>
		/// <param name="size"></param>
		/// <param name="lowerCase"></param>
		/// <returns></returns>

		public static string RandomString(int size, bool lowerCase = false)
		{
			var builder = new StringBuilder(size);

			// Unicode/ASCII Letters are divided into two blocks
			// (Letters 65–90 / 97–122):   
			// The first group containing the uppercase letters and
			// the second group containing the lowercase.  

			// char is a single Unicode character  
			char offset = lowerCase ? 'a' : 'A';
			const int lettersOffset = 26; // A...Z or a..z: length = 26  

			for (var i = 0; i < size; i++)
			{
				var @char = (char)_random.Next(offset, offset + lettersOffset);
				builder.Append(@char);
			}

			return lowerCase ? builder.ToString().ToLower() : builder.ToString();
		}

		/// <summary>
		/// Generates a random number within a range.   
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>

		public static int RandomNumber(int min, int max)
		{
			return _random.Next(min, max);
		}


		/// <summary>
		/// Password Generator
		/// </summary>
		/// <returns></returns>
		public static string RandomPassword()
		{
			var passwordBuilder = new StringBuilder();

			// 4-Letters lower case   
			passwordBuilder.Append(RandomString(4, true));

			// 4-Digits between 1000 and 9999  
			passwordBuilder.Append(RandomNumber(1000, 9999));

			// 2-Letters upper case  
			passwordBuilder.Append(RandomString(2));
			return passwordBuilder.ToString();
		}
	}
}
