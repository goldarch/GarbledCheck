# GarbledCheck
中文乱码遍历转换尝试

通过遍历本机编码，对中文乱码进行尝试性的转换
使用者可以通过观察转换的结果，找到正确的原始编码

![image](https://raw.githubusercontent.com/goldarch/GarbledCheck/master/GarbledCheck/img-folder/ui01.png)  

另外，网上流转的直接判断BOM的代码：
/// <summary>
/// Determines a text file's encoding by analyzing its byte order mark (BOM).
/// Defaults to ASCII when detection of the text file's endianness fails.
/// </summary>
/// <param name="filename">The text file to analyze.</param>
/// <returns>The detected encoding.</returns>
public static Encoding GetEncoding(string filename)
{
    // Read the BOM
    var bom = new byte[4];
    using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
    {
        file.Read(bom, 0, 4);
    }

    // Analyze the BOM
    if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
    if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
    if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
    if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
    if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
    return Encoding.ASCII;
}

相关的讨论：
https://stackoverflow.com/questions/4942825/xdocument-saving-xml-to-file-without-bom
https://www.codeproject.com/Articles/5302027/Removing-a-BOMB-from-Your-Text
