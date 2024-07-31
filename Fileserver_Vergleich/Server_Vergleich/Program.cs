using Server_Vergleich.Server_Tests;
using Server_Vergleich.DataStructs;

// prepare Date

int datacapSafe = 10,
    datacapGet = 10;

string minioEndpoint = "localhost:9000";
string minioAccessKey = "minio";
string minioSecretKey = "minio123";
string bucketName = "testbucket";
string localFilePath = "C:\\Custom\\Projekte\\SystemArchitecture\\Musik";
string glusterMountPoint = "/mnt/gluster";
string nfsMountPoint = "/mnt/nfs";


Times
    minIo_t = new Times(),
    gluster_t = new Times(),
    nsf_t = new Times();




List<string> _Files = new List<string>();
string[] getList = new string[datacapGet];


Console.WriteLine("Start Preparing Date");

string[] RawFiles = Directory.GetFiles(localFilePath, "*.mp3");


for (int i = 0; i < datacapSafe; i++)
{
    _Files.Add(RawFiles[i]);
    Console.WriteLine(RawFiles[i]);
}

Console.WriteLine("--------------------------");
Random random = new Random();

for (int j = 0; j < _Files.Count; j++)
{
    getList[j] = _Files[random.Next(0, _Files.Count)];
   Console.WriteLine(getList[j]);
}

Console.WriteLine("End prepare data");


// Test MinIo

Console.WriteLine("----------------------------");
Console.WriteLine("Start Minio Test");
MinIoTests _MinIoTest = new MinIoTests();

minIo_t = _MinIoTest.RunTest(_Files, getList, minIo_t).Result;

Console.WriteLine("Ende Minio Test ");
Console.WriteLine("----------------------------");
// Test Gluster

Console.WriteLine("----------------------------");
Console.WriteLine("Start Gluster Test");
GlusterTests _GlusterTest = new GlusterTests();

gluster_t = _GlusterTest.RunTest(_Files, getList, gluster_t);
Console.WriteLine("Ende Gluster Test ");
Console.WriteLine("----------------------------");
// MySql

Console.WriteLine("----------------------------");
Console.WriteLine("Start MySql DB Test");

NsfTests _NsfTest = new NsfTests();

nsf_t = _NsfTest.RunTest(_Files, getList, nsf_t);
Console.WriteLine("Ende MySql DB Test ");
Console.WriteLine("----------------------------");









// Show Stats 


Console.WriteLine("Minio        Safe    Time: " + minIo_t.diffWrite.TotalSeconds);
Console.WriteLine("Gluster      Safe    Time: " + gluster_t.diffWrite.TotalSeconds);
Console.WriteLine("NSF          Safe    Time: " + nsf_t.diffWrite.TotalSeconds);

Console.WriteLine();
Console.WriteLine("-----------------------------------------------------");
Console.WriteLine();

Console.WriteLine("Minio        Get     Time: " + minIo_t.diffRead.TotalSeconds);
Console.WriteLine("Gluster      Get     Time: " + gluster_t.diffRead.TotalSeconds);
Console.WriteLine("NSF          Get     Time: " + nsf_t.diffRead.TotalSeconds);

Console.ReadLine();