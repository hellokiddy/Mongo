using System;

namespace Mongo.Common.Net
{
	public class Package
	{
		public PackageType type;
		public int length;
		public byte[] body;

		public Package (PackageType type, byte[] body)
		{
			this.type = type;
			this.length = body.Length;
			this.body = body;
		}
	}
}