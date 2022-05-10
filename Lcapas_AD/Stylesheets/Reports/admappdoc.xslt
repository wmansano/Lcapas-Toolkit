<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template name="formatdate">
    <xsl:param name="InstDurationDate" />
    <!-- input format xslt datetime string -->
    <!-- output format MM yyyy -->

    <xsl:variable name="mm">
      <xsl:value-of select="substring($InstDurationDate,0,4)" />
    </xsl:variable>

    <xsl:variable name="yyyy">
      <xsl:value-of select="substring($InstDurationDate,8,5)" />
    </xsl:variable>

    <xsl:value-of select="concat($mm,' ', $yyyy)" />
  </xsl:template>
  <xsl:template match="/">
    <?mso-application progid="Word.Document"?>
    <pkg:package xmlns:pkg="http://schemas.microsoft.com/office/2006/xmlPackage">
      <pkg:part pkg:name="/_rels/.rels" pkg:contentType="application/vnd.openxmlformats-package.relationships+xml" pkg:padding="512">
        <pkg:xmlData>
          <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
            <Relationship Id="rId3" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties" Target="docProps/app.xml"/>
            <Relationship Id="rId2" Type="http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties" Target="docProps/core.xml"/>
            <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="word/document.xml"/>
          </Relationships>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/_rels/document.xml.rels" pkg:contentType="application/vnd.openxmlformats-package.relationships+xml" pkg:padding="256">
        <pkg:xmlData>
          <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
            <Relationship Id="rId8" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/header" Target="header2.xml"/>
            <Relationship Id="rId13" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/fontTable" Target="fontTable.xml"/>
            <Relationship Id="rId3" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/settings" Target="settings.xml"/>
            <Relationship Id="rId7" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/header" Target="header1.xml"/>
            <Relationship Id="rId12" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer" Target="footer3.xml"/>
            <Relationship Id="rId2" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles" Target="styles.xml"/>
            <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXml" Target="../customXml/item1.xml"/>
            <Relationship Id="rId6" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/endnotes" Target="endnotes.xml"/>
            <Relationship Id="rId11" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/header" Target="header3.xml"/>
            <Relationship Id="rId5" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/footnotes" Target="footnotes.xml"/>
            <Relationship Id="rId10" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer" Target="footer2.xml"/>
            <Relationship Id="rId4" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/webSettings" Target="webSettings.xml"/>
            <Relationship Id="rId9" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/footer" Target="footer1.xml"/>
            <Relationship Id="rId14" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/theme" Target="theme/theme1.xml"/>
          </Relationships>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/document.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml">
        <pkg:xmlData>
          <w:document mc:Ignorable="w14 wp14" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp14="http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:wpg="http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" xmlns:wpi="http://schemas.microsoft.com/office/word/2010/wordprocessingInk" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wps="http://schemas.microsoft.com/office/word/2010/wordprocessingShape">
            <w:body>
              <xsl:for-each select="ArrayOfApplicationSchemaFilter/ApplicationSchemaFilter">
                <w:tbl>
                  <w:tblPr>
                    <w:tblW w:w="5000" w:type="pct"/>
                    <w:tblLook w:val="04A0" w:firstRow="1" w:lastRow="0" w:firstColumn="1" w:lastColumn="0" w:noHBand="0" w:noVBand="1"/>
                  </w:tblPr>
                  <w:tblGrid>
                    <w:gridCol w:w="3158"/>
                    <w:gridCol w:w="1630"/>
                    <w:gridCol w:w="1080"/>
                    <w:gridCol w:w="23"/>
                    <w:gridCol w:w="787"/>
                    <w:gridCol w:w="2898"/>
                  </w:tblGrid>
                  <w:tr w:rsidR="00BA603D" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="4788" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00BA603D" w:rsidRPr="004423C8" w:rsidRDefault="003E2AB7" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                        </w:pPr>
                        <w:bookmarkStart w:id="0" w:name="_GoBack"/>
                        <w:bookmarkEnd w:id="0"/>
                        <w:r w:rsidRPr="004423C8">
                          <w:t>DATA SHEET</w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="4788" w:type="dxa"/>
                        <w:gridSpan w:val="4"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00BA603D" w:rsidRPr="004423C8" w:rsidRDefault="00BA603D" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:jc w:val="right"/>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:fldChar w:fldCharType="begin"/>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:instrText xml:space="preserve"> DATE \@ "d MMM yyyy" </w:instrText>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:fldChar w:fldCharType="separate"/>
                        </w:r>
                        <w:r w:rsidR="000B7F84">
                          <w:rPr>
                            <w:noProof/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./datesubmitted"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:fldChar w:fldCharType="end"/>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="425"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="9576" w:type="dxa"/>
                        <w:gridSpan w:val="6"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2"/>
                        <w:vAlign w:val="center"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:b/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:b/>
                          </w:rPr>
                          <w:t>PERSONAL INFORMATION:</w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00A5009D" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="6678" w:type="dxa"/>
                        <w:gridSpan w:val="5"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00A5009D" w:rsidRPr="004423C8" w:rsidRDefault="00A5009D" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">NAME (Last, First Middle):  </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./lastName"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:t xml:space="preserve">, </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./firstName"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:t xml:space="preserve"> </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./middleName"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="2898" w:type="dxa"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00A5009D" w:rsidRPr="004423C8" w:rsidRDefault="00A5009D" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">LC ID: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <!--<xsl:value-of select="./previousStudentNumber"/>-->
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="9576" w:type="dxa"/>
                        <w:gridSpan w:val="6"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">FORMER NAMES: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./FormerNames"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5891" w:type="dxa"/>
                        <w:gridSpan w:val="4"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">ADDR LN1: </w:t>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="begin"/>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:instrText xml:space="preserve"> MERGEFIELD  PermAddr1  \* MERGEFORMAT </w:instrText>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="separate"/>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./PermAddr1"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="end"/>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3685" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">ADDR LN 2: </w:t>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="begin"/>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:instrText xml:space="preserve"> MERGEFIELD  PermAddr2  \* MERGEFORMAT </w:instrText>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="separate"/>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./PermAddr2"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="end"/>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5891" w:type="dxa"/>
                        <w:gridSpan w:val="4"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">CITY, PROV POSTAL: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./PermAddrCity"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="end"/>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:if test="./PermAddrStateID != ''">
                              <xsl:text xml:space="preserve">, </xsl:text>
                            </xsl:if>
                          </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./PermAddrStateID"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:t xml:space="preserve"> </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./PermAddrPostalCode"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3685" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">COUNTRY: </w:t>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve"/>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="begin"/>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:instrText xml:space="preserve"> MERGEFIELD  PermAddrCountryID  \* MERGEFORMAT </w:instrText>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="separate"/>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./PermAddrCountryID"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidR="00872426" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:fldChar w:fldCharType="end"/>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5891" w:type="dxa"/>
                        <w:gridSpan w:val="4"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">PERM. PHONE: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./PermAddrPhone_CountryPrefixCode"/>
                            <xsl:if test="./PermAddrPhone_AreaCode != ''">
                              <xsl:text> </xsl:text>
                            </xsl:if>
                            <xsl:value-of select="./PermAddrPhone_AreaCode"/>
                            <xsl:if test="./PermAddrPhone_Exchange != ''">
                              <xsl:text>-</xsl:text>
                            </xsl:if>
                            <xsl:value-of select="./PermAddrPhone_Exchange"/>
                            <xsl:if test="./PermAddrPhone_Number != ''">
                              <xsl:text>-</xsl:text>
                            </xsl:if>
                            <xsl:value-of select="./PermAddrPhone_Number"/>
                            <xsl:if test="./PermAddrPhone_Extension != ''">
                              <xsl:text> ext: </xsl:text>
                            </xsl:if>
                            <xsl:value-of select="./PermAddrPhone_Extension"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3685" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">OTHER PHONE: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./OtherPhone_CountryPrefixCode"/>
                            <xsl:if test="./OtherPhone_AreaCode != ''">
                              <xsl:text> </xsl:text>
                            </xsl:if>
                            <xsl:value-of select="./OtherPhone_AreaCode"/>
                            <xsl:if test="./OtherPhone_Exchange != ''">
                              <xsl:text>-</xsl:text>
                            </xsl:if>
                            <xsl:value-of select="./OtherPhone_Exchange"/>
                            <xsl:if test="./OtherPhone_Number != ''">
                              <xsl:text>-</xsl:text>
                            </xsl:if>
                            <xsl:value-of select="./OtherPhone_Number"/>
                            <xsl:if test="./OtherPhone_Extension != ''">
                              <xsl:text> ext: </xsl:text>
                            </xsl:if>
                            <xsl:value-of select="./OtherPhone_Extension"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5891" w:type="dxa"/>
                        <w:gridSpan w:val="4"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">GENDER: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./sexID"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3685" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">BIRTHDATE (d/m/y): </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./dob_Day"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:t>
                            <xsl:if test="./dob_Month != ''">
                              <xsl:text>/</xsl:text>
                            </xsl:if>
                          </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./dob_Month"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:t>
                            <xsl:if test="./dob_Year != ''">
                              <xsl:text>/</xsl:text>
                            </xsl:if>
                          </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./dob_Year"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5891" w:type="dxa"/>
                        <w:gridSpan w:val="4"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">ABORIGINAL STATUS: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./aboriginalStatus"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3685" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">EMAIL: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./email"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="425"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="9576" w:type="dxa"/>
                        <w:gridSpan w:val="6"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2"/>
                        <w:vAlign w:val="center"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:b/>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:b/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">APPLICATION INFORMATION: </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3158" w:type="dxa"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">TERM: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./entryTerm"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="2733" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">TYPE OF STUDENT: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./StudentTypeID"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3685" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">PREVIOUSLY APPLIED: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./previousApplicant"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5891" w:type="dxa"/>
                        <w:gridSpan w:val="4"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00362C64" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">PROGRAM: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./Program"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3685" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">LOCATION: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./Location"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5891" w:type="dxa"/>
                        <w:gridSpan w:val="4"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00362C64" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">ADMIT STATUS: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./StartYear"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3685" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00855765" w:rsidRPr="004423C8" w:rsidRDefault="00855765" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">RESIDENCY: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./ResidenceStatusCode"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <!--<w:tr w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="425"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="9576" w:type="dxa"/>
                        <w:gridSpan w:val="6"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2"/>
                        <w:vAlign w:val="center"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00362C64" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:b/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">PAYMENT INFORMATION: </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5868" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00362C64" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">CARD TYPE: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./cardType"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3708" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00362C64" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">CONFIRMATON #: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./refNumber"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="9576" w:type="dxa"/>
                        <w:gridSpan w:val="6"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00362C64" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">NAME ON CARD: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./nameOnCard"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>-->
                  <w:tr w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="425"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="9576" w:type="dxa"/>
                        <w:gridSpan w:val="6"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2"/>
                        <w:vAlign w:val="center"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00362C64" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:b/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">CITIZENSHIP INFORMATION: </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3158" w:type="dxa"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00362C64" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">CITIZENSHIP: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./CitizenshipStatusCode"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="2710" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">COUNTRY: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./CitizenshipCountryCode"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3708" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">RESIDENCE COUNTRY: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./ResidenceCountrycode"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5868" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">ENTRY DATE (d/m/y): </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./CountryEntryDate_Day"/>
                          </w:t>
                        </w:r>
                        <xsl:if test="./CountryEntryDate_Year != ''">
                          <w:r w:rsidRPr="004423C8">
                            <w:t xml:space="preserve">/</w:t>
                          </w:r>
                        </xsl:if>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./CountryEntryDate_Month"/>
                          </w:t>
                        </w:r>
                        <xsl:if test="./CountryEntryDate_Month != ''">
                          <w:r w:rsidRPr="004423C8">
                            <w:t xml:space="preserve">/</w:t>
                          </w:r>
                        </xsl:if>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./CountryEntryDate_Year"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3708" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00362C64" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">FIRST LANGUAGE: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./firstLanguage"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="425"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="9576" w:type="dxa"/>
                        <w:gridSpan w:val="6"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2"/>
                        <w:vAlign w:val="center"/>
                      </w:tcPr>
                      <w:p w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:b/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">OTHER INFORMATION: </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5868" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:b/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">MARITAL STATUS: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./maritalStatus"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3708" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">AB ED ID: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./AlbertaEducationNumber"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5868" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">ALTERNATE CONTACT: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./EmerContactName"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3708" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:b/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">ALT CONTACT PH: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./EmerContactPhone"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                  <w:tr w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:tblPrEx>
                      <w:tblBorders>
                        <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                        <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                      </w:tblBorders>
                    </w:tblPrEx>
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5868" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:b/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">INFLUENCED TO APPLY: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./FirstLearned"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="3708" w:type="dxa"/>
                        <w:gridSpan w:val="3"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00872426" w:rsidRPr="004423C8" w:rsidRDefault="00872426" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:rPr>
                            <w:b/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:color w:val="808080"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t xml:space="preserve">DISABILITY: </w:t>
                        </w:r>
                        <w:r w:rsidR="004423C8" w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                            <w:color w:val="000000"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./disability"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                </w:tbl>
                <w:p w:rsidR="00DD4C68" w:rsidRDefault="00DD4C68" w:rsidP="00872426">
                  <w:pPr>
                    <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                  </w:pPr>
                </w:p>
                <w:p w:rsidR="00DD4C68" w:rsidRDefault="00DD4C68" w:rsidP="00872426">
                  <w:pPr>
                    <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                  </w:pPr>
                  <w:r>
                    <w:t xml:space="preserve">For database use only: </w:t>
                  </w:r>
                </w:p>
                <w:tbl>
                  <w:tblPr>
                    <w:tblStyle w:val="TableGrid"/>
                    <w:tblW w:w="5000" w:type="pct"/>
                    <w:tblBorders>
                      <w:top w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:left w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:bottom w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:right w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:insideH w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:insideV w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                    </w:tblBorders>
                    <w:tblLook w:val="04A0" w:firstRow="1" w:lastRow="0" w:firstColumn="1" w:lastColumn="0" w:noHBand="0" w:noVBand="1"/>
                  </w:tblPr>
                  <w:tblGrid>
                    <w:gridCol w:w="1674"/>
                    <w:gridCol w:w="1315"/>
                    <w:gridCol w:w="1545"/>
                    <w:gridCol w:w="1288"/>
                    <w:gridCol w:w="74"/>
                    <w:gridCol w:w="1801"/>
                    <w:gridCol w:w="1879"/>
                  </w:tblGrid>
                  <w:tr w:rsidR="00DD4C68" w:rsidRPr="00872426" w:rsidTr="00E84AB8">
                    <w:trPr>
                      <w:trHeight w:val="505"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="1174" w:type="dxa"/>
                      </w:tcPr>
                      <w:p w:rsidR="00DD4C68" w:rsidRPr="00872426" w:rsidRDefault="00DD4C68">
                        <w:pPr>
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="00872426">
                          <w:rPr>
                            <w:color w:val="808080" w:themeColor="background1" w:themeShade="80"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>Submitted:</w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="1644" w:type="dxa"/>
                        <w:vAlign w:val="center"/>
                      </w:tcPr>
                      <w:p w:rsidR="00DD4C68" w:rsidRPr="00872426" w:rsidRDefault="00DD4C68" w:rsidP="00872426">
                        <w:pPr>
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="1189" w:type="dxa"/>
                      </w:tcPr>
                      <w:p w:rsidR="00DD4C68" w:rsidRPr="00872426" w:rsidRDefault="00DD4C68">
                        <w:pPr>
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="00872426">
                          <w:rPr>
                            <w:color w:val="808080" w:themeColor="background1" w:themeShade="80"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>Exported:</w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="1605" w:type="dxa"/>
                        <w:vAlign w:val="center"/>
                      </w:tcPr>
                      <w:p w:rsidR="00DD4C68" w:rsidRPr="00872426" w:rsidRDefault="00DD4C68" w:rsidP="00872426">
                        <w:pPr>
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="1512" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                      </w:tcPr>
                      <w:p w:rsidR="00DD4C68" w:rsidRPr="00872426" w:rsidRDefault="00DD4C68">
                        <w:pPr>
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                        <w:r w:rsidRPr="00872426">
                          <w:rPr>
                            <w:color w:val="808080" w:themeColor="background1" w:themeShade="80"/>
                            <w:sz w:val="20"/>
                          </w:rPr>
                          <w:t>Application ID:</w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="2452" w:type="dxa"/>
                        <w:vAlign w:val="center"/>
                      </w:tcPr>
                      <w:p w:rsidR="00DD4C68" w:rsidRPr="00872426" w:rsidRDefault="00DD4C68" w:rsidP="00872426">
                        <w:pPr>
                          <w:rPr>
                            <w:sz w:val="20"/>
                          </w:rPr>
                        </w:pPr>
                      </w:p>
                    </w:tc>
                  </w:tr>
                </w:tbl>
                <w:p w:rsidR="00D27715" w:rsidRPr="00964952" w:rsidRDefault="0005124B" w:rsidP="00964952" break-after="page">
                  <w:pPr>
                    <w:lastRenderedPageBreak/>
                    <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                  </w:pPr>
                  <w:bookmarkStart w:id="0" w:name="_GoBack"/>
                  <w:bookmarkEnd w:id="0"/>
                  <w:r>
                    <w:br w:type="page"/>
                  </w:r>
                </w:p>
                <w:tbl>
                  <w:tblPr>
                    <w:tblW w:w="5000" w:type="pct"/>
                    <w:tblLook w:val="04A0" w:firstRow="1" w:lastRow="0" w:firstColumn="1" w:lastColumn="0" w:noHBand="0" w:noVBand="1"/>
                  </w:tblPr>
                  <w:tblGrid>
                    <w:gridCol w:w="5656"/>
                    <w:gridCol w:w="3920"/>
                  </w:tblGrid>
                  <w:tr w:rsidR="00BA603D" w:rsidRPr="004423C8" w:rsidTr="004423C8">
                    <w:trPr>
                      <w:trHeight w:val="504"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="4788" w:type="dxa"/>
                        <w:gridSpan w:val="2"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00BA603D" w:rsidRPr="004423C8" w:rsidRDefault="003E2AB7" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                        </w:pPr>
                        <w:bookmarkStart w:id="0" w:name="_GoBack"/>
                        <w:bookmarkEnd w:id="0"/>
                        <w:r w:rsidRPr="004423C8">
                          <w:t>EDUCATION HISTORY</w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="5656" w:type="dxa"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF"/>
                      </w:tcPr>
                      <w:p w:rsidR="003E580D" w:rsidRPr="004423C8" w:rsidRDefault="003E580D" w:rsidP="00E44861">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./lastName"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:t xml:space="preserve">, </w:t>
                        </w:r>
                        <w:r>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./firstName"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:t xml:space="preserve"> </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:rPr>
                            <w:noProof/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./middleName"/>
                          </w:t>
                        </w:r>
                      </w:p>
                    </w:tc>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="4788" w:type="dxa"/>
                        <w:gridSpan w:val="4"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="auto"/>
                      </w:tcPr>
                      <w:p w:rsidR="00BA603D" w:rsidRPr="004423C8" w:rsidRDefault="00BA603D" w:rsidP="004423C8">
                        <w:pPr>
                          <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                          <w:jc w:val="right"/>
                        </w:pPr>
                        <w:r w:rsidRPr="004423C8">
                          <w:fldChar w:fldCharType="begin"/>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:instrText xml:space="preserve"> DATE \@ "d/M/yyyy" </w:instrText>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:fldChar w:fldCharType="separate"/>
                        </w:r>
                        <w:r w:rsidR="000B7F84">
                          <w:rPr>
                            <w:noProof/>
                          </w:rPr>
                          <w:t>
                            <xsl:value-of select="./datesubmitted"/>
                          </w:t>
                        </w:r>
                        <w:r w:rsidRPr="004423C8">
                          <w:fldChar w:fldCharType="end"/>
                        </w:r>
                      </w:p>
                    </w:tc>
                  </w:tr>
                </w:tbl>
                <w:tbl>
                  <w:tblPr>
                    <w:tblStyle w:val="TableGrid"/>
                    <w:tblW w:w="5000" w:type="pct"/>
                    <w:tblBorders>
                      <w:top w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:left w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:bottom w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:right w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:insideH w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:insideV w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                    </w:tblBorders>
                    <w:tblLook w:val="04A0" w:firstRow="1" w:lastRow="0" w:firstColumn="1" w:lastColumn="0" w:noHBand="0" w:noVBand="1"/>
                  </w:tblPr>
                  <w:tblGrid>
                    <w:gridCol w:w="9360"/>
                  </w:tblGrid>
                  <w:tr w:rsidR="00326562" w:rsidRPr="00401998" w:rsidTr="00326562">
                    <w:trPr>
                      <w:trHeight w:val="672"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="9360" w:type="dxa"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1"/>
                        <w:tcMar>
                          <w:left w:w="0" w:type="dxa"/>
                          <w:right w:w="0" w:type="dxa"/>
                        </w:tcMar>
                      </w:tcPr>
                      <w:tbl>
                        <w:tblPr>
                          <w:tblW w:w="10000" w:type="dxa"/>
                          <w:tblLook w:val="0000" w:firstRow="0" w:lastRow="0" w:firstColumn="0" w:lastColumn="0" w:noHBand="0" w:noVBand="0"/>
                        </w:tblPr>
                        <w:tblGrid>
                          <w:gridCol w:w="4000"/>
                          <w:gridCol w:w="1500"/>
                          <w:gridCol w:w="2000"/>
                          <w:gridCol w:w="2500"/>
                        </w:tblGrid>
                        <w:tr w:rsidR="00326562" w:rsidRPr="00326562" w:rsidTr="00326562">
                          <w:trPr>
                            <w:trHeight w:hRule="exact" w:val="315"/>
                          </w:trPr>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="4000" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:bookmarkStart w:id="0" w:name="institutions"/>
                              <w:bookmarkEnd w:id="0"/>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t xml:space="preserve">Institutions </w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="1500" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t>Institution ID</w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="2000" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t>Location</w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="2500" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t>Duration</w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                        </w:tr>
                        <xsl:for-each select="ApplicationInstitutions/ApplicationInstitution">
                          <w:tr w:rsidR="00326562" w:rsidTr="00D22805">
                            <w:trPr>
                              <w:trHeight w:hRule="exact" w:val="240"/>
                            </w:trPr>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="4000" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00D22805">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:proofErr w:type="spellStart"/>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:value-of select="InstitutionName"/>
                                  </w:t>
                                </w:r>
                                <w:proofErr w:type="spellEnd"/>
                              </w:p>
                            </w:tc>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="1500" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:proofErr w:type="spellStart"/>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:value-of select="InstitutionID"/>
                                  </w:t>
                                </w:r>
                                <w:proofErr w:type="spellEnd"/>
                              </w:p>
                            </w:tc>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="2000" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00D22805">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:proofErr w:type="spellStart"/>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:value-of select="InstitutionAddressCity"/>
                                    <xsl:if test="./InstitutionAddressProvinceState != ''">
                                      <xsl:text>, </xsl:text>
                                    </xsl:if>
                                    <xsl:value-of select="InstitutionAddressProvinceState"/>
                                  </w:t>
                                </w:r>
                                <w:proofErr w:type="spellEnd"/>
                              </w:p>
                            </w:tc>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="2500" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00D22805">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:call-template name="formatdate">
                                      <xsl:with-param name="InstDurationDate" select="StartYear"/>
                                    </xsl:call-template>
                                  </w:t>
                                </w:r>
                                <w:r w:rsidRPr="004423C8">
                                  <xsl:if test="./EndYear != ''">
                                    <w:t xml:space="preserve"> - </w:t>
                                  </xsl:if>
                                </w:r>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:call-template name="formatdate">
                                      <xsl:with-param name="InstDurationDate" select="EndYear"/>
                                    </xsl:call-template>
                                  </w:t>
                                </w:r>
                                <w:bookmarkStart w:id="1" w:name="_GoBack"/>
                                <w:bookmarkEnd w:id="1"/>
                              </w:p>
                            </w:tc>
                          </w:tr>
                        </xsl:for-each>
                      </w:tbl>
                      <w:p w:rsidR="00326562" w:rsidRPr="00BE48CA" w:rsidRDefault="00326562" w:rsidP="00D22805">
                        <w:pPr>
                          <w:rPr>
                            <w:sz w:val="18"/>
                            <w:szCs w:val="18"/>
                          </w:rPr>
                        </w:pPr>
                      </w:p>
                    </w:tc>
                  </w:tr>
                </w:tbl>
                <w:p />
                <!--</xsl:for-each>-->
                <w:tbl>
                  <w:tblPr>
                    <w:tblStyle w:val="TableGrid"/>
                    <w:tblW w:w="5000" w:type="pct"/>
                    <w:tblBorders>
                      <w:top w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:left w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:bottom w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:right w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:insideH w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                      <w:insideV w:val="none" w:sz="0" w:space="0" w:color="auto"/>
                    </w:tblBorders>
                    <w:tblLook w:val="04A0" w:firstRow="1" w:lastRow="0" w:firstColumn="1" w:lastColumn="0" w:noHBand="0" w:noVBand="1"/>
                  </w:tblPr>
                  <w:tblGrid>
                    <w:gridCol w:w="9360"/>
                  </w:tblGrid>
                  <w:tr w:rsidR="00326562" w:rsidRPr="00401998" w:rsidTr="00326562">
                    <w:trPr>
                      <w:trHeight w:val="672"/>
                    </w:trPr>
                    <w:tc>
                      <w:tcPr>
                        <w:tcW w:w="9360" w:type="dxa"/>
                        <w:shd w:val="clear" w:color="auto" w:fill="FFFFFF" w:themeFill="background1"/>
                        <w:tcMar>
                          <w:left w:w="0" w:type="dxa"/>
                          <w:right w:w="0" w:type="dxa"/>
                        </w:tcMar>
                      </w:tcPr>
                      <!--<w:tbl break-after="page">
                        <w:tblPr>
                          <w:tblW w:w="10000" w:type="dxa"/>
                          <w:tblLook w:val="0000" w:firstRow="0" w:lastRow="0" w:firstColumn="0" w:lastColumn="0" w:noHBand="0" w:noVBand="0"/>
                        </w:tblPr>
                        <w:tblGrid>
                          <w:gridCol w:w="3513"/>
                          <w:gridCol w:w="1230"/>
                          <w:gridCol w:w="702"/>
                          <w:gridCol w:w="702"/>
                          <w:gridCol w:w="1142"/>
                          <w:gridCol w:w="1493"/>
                        </w:tblGrid>
                        <w:tr w:rsidR="00326562" w:rsidRPr="00326562" w:rsidTr="00326562">
                          <w:trPr>
                            <w:trHeight w:hRule="exact" w:val="320"/>
                          </w:trPr>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="3513" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:bookmarkStart w:id="2" w:name="courses"/>
                              <w:bookmarkEnd w:id="2"/>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t>Courses</w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="1230" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:proofErr w:type="spellStart"/>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t>Course</w:t>
                              </w:r>
                              <w:proofErr w:type="spellEnd"/>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t xml:space="preserve"> ID</w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="702" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t>Grade</w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="702" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t>Credit</w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="1142" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t>Status</w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                          <w:tc>
                            <w:tcPr>
                              <w:tcW w:w="1493" w:type="dxa"/>
                              <w:shd w:val="clear" w:color="auto" w:fill="F2F2F2" w:themeFill="background1" w:themeFillShade="F2"/>
                              <w:vAlign w:val="center"/>
                            </w:tcPr>
                            <w:p w:rsidR="00326562" w:rsidRPr="00326562" w:rsidRDefault="00326562" w:rsidP="00326562">
                              <w:pPr>
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                              </w:pPr>
                              <w:r w:rsidRPr="00326562">
                                <w:rPr>
                                  <w:b/>
                                  <w:sz w:val="18"/>
                                  <w:szCs w:val="16"/>
                                </w:rPr>
                                <w:t>End Date</w:t>
                              </w:r>
                            </w:p>
                          </w:tc>
                        </w:tr>
                        <xsl:for-each select="ApplicationCourses/ApplicationCourse">
                          <w:tr w:rsidR="00326562" w:rsidTr="00326562">
                            <w:trPr>
                              <w:trHeight w:hRule="exact" w:val="260"/>
                            </w:trPr>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="3513" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00D22805">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:proofErr w:type="spellStart"/>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:value-of select="CourseName"/>
                                  </w:t>
                                </w:r>
                                <w:proofErr w:type="spellEnd"/>
                              </w:p>
                            </w:tc>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="1230" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00D22805">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:proofErr w:type="spellStart"/>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:value-of select="AgencyCourseID"/>
                                  </w:t>
                                </w:r>
                                <w:proofErr w:type="spellEnd"/>
                              </w:p>
                            </w:tc>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="702" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00D22805">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:proofErr w:type="spellStart"/>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:value-of select="CourseAcademicGrade"/>
                                  </w:t>
                                </w:r>
                                <w:proofErr w:type="spellEnd"/>
                              </w:p>
                            </w:tc>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="702" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00D22805">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:proofErr w:type="spellStart"/>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:value-of select="CourseCreditEarned"/>
                                  </w:t>
                                </w:r>
                                <w:proofErr w:type="spellEnd"/>
                              </w:p>
                            </w:tc>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="1142" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00D22805">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:proofErr w:type="spellStart"/>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:value-of select="CourseNarrativeExplantionGrade"/>
                                  </w:t>
                                </w:r>
                                <w:proofErr w:type="spellEnd"/>
                              </w:p>
                            </w:tc>
                            <w:tc>
                              <w:tcPr>
                                <w:tcW w:w="1493" w:type="dxa"/>
                              </w:tcPr>
                              <w:p w:rsidR="00326562" w:rsidRDefault="00326562" w:rsidP="00D22805">
                                <w:pPr>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                </w:pPr>
                                <w:r>
                                  <w:rPr>
                                    <w:sz w:val="16"/>
                                    <w:szCs w:val="16"/>
                                  </w:rPr>
                                  <w:t>
                                    <xsl:value-of select="CourseEndDate"/>
                                  </w:t>
                                </w:r>
                              </w:p>
                            </w:tc>
                          </w:tr>
                        </xsl:for-each>
                      </w:tbl>-->
                      <w:p w:rsidR="00326562" w:rsidRPr="00BE48CA" w:rsidRDefault="00326562" w:rsidP="00D22805">
                        <w:pPr>
                          <w:rPr>
                            <w:sz w:val="18"/>
                            <w:szCs w:val="18"/>
                          </w:rPr>
                        </w:pPr>
                      </w:p>
                    </w:tc>
                  </w:tr>
                </w:tbl>
                <w:p />

                <w:p w:rsidR="00D27715" w:rsidRPr="00964952" w:rsidRDefault="0005124B" w:rsidP="00964952" break-after="page">
                  <w:pPr>
                    <w:lastRenderedPageBreak/>
                    <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                  </w:pPr>
                  <w:bookmarkStart w:id="0" w:name="_GoBack"/>
                  <w:bookmarkEnd w:id="0"/>
                  <w:r>
                    <w:br w:type="page"/>
                  </w:r>
                </w:p>
              </xsl:for-each>
              <w:sectPr w:rsidR="00E84AB8" w:rsidSect="000D5698">
                <w:headerReference w:type="even" r:id="rId7"/>
                <w:headerReference w:type="default" r:id="rId8"/>
                <w:footerReference w:type="even" r:id="rId9"/>
                <w:footerReference w:type="default" r:id="rId10"/>
                <w:headerReference w:type="first" r:id="rId11"/>
                <w:footerReference w:type="first" r:id="rId12"/>
                <w:pgSz w:w="12240" w:h="15840"/>
                <w:pgMar w:top="1440" w:right="1440" w:bottom="1440" w:left="1440" w:header="540" w:footer="720" w:gutter="0"/>
                <w:cols w:space="720"/>
                <w:docGrid w:linePitch="360"/>
              </w:sectPr>
            </w:body>
          </w:document>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/header3.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.header+xml">
        <pkg:xmlData>
          <w:hdr mc:Ignorable="w14 wp14" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp14="http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:wpg="http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" xmlns:wpi="http://schemas.microsoft.com/office/word/2010/wordprocessingInk" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wps="http://schemas.microsoft.com/office/word/2010/wordprocessingShape">
            <w:p w:rsidR="00BE48CA" w:rsidRDefault="00BE48CA">
              <w:pPr>
                <w:pStyle w:val="Header"/>
              </w:pPr>
            </w:p>
          </w:hdr>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/footer2.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.footer+xml">
        <pkg:xmlData>
          <w:ftr mc:Ignorable="w14 wp14" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp14="http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:wpg="http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" xmlns:wpi="http://schemas.microsoft.com/office/word/2010/wordprocessingInk" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wps="http://schemas.microsoft.com/office/word/2010/wordprocessingShape">
            <w:p w:rsidR="005C6E85" w:rsidRDefault="005C6E85" w:rsidP="004423C8">
              <w:pPr>
                <w:pStyle w:val="Footer"/>
                <w:jc w:val="center"/>
              </w:pPr>
              <w:r>
                <w:t>Registrar’s Office</w:t>
              </w:r>
              <w:r w:rsidR="004423C8">
                <w:tab/>
              </w:r>
              <w:r>
                <w:t>Lethbridge College</w:t>
              </w:r>
              <w:r w:rsidR="004423C8">
                <w:tab/>
              </w:r>
              <w:r>
                <w:fldChar w:fldCharType="begin"/>
              </w:r>
              <w:r>
                <w:instrText xml:space="preserve"> DATE \@ "d/M/yyyy" </w:instrText>
              </w:r>
              <w:r>
                <w:fldChar w:fldCharType="separate"/>
              </w:r>
              <w:r w:rsidR="000B7F84">
                <w:rPr>
                  <w:noProof/>
                </w:rPr>
                <w:t>
                  <xsl:value-of select="./datesubmitted"/>
                </w:t>
              </w:r>
              <w:r>
                <w:fldChar w:fldCharType="end"/>
              </w:r>
            </w:p>
          </w:ftr>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/footer1.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.footer+xml">
        <pkg:xmlData>
          <w:ftr mc:Ignorable="w14 wp14" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp14="http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:wpg="http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" xmlns:wpi="http://schemas.microsoft.com/office/word/2010/wordprocessingInk" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wps="http://schemas.microsoft.com/office/word/2010/wordprocessingShape">
            <w:p w:rsidR="00BE48CA" w:rsidRDefault="00BE48CA">
              <w:pPr>
                <w:pStyle w:val="Footer"/>
              </w:pPr>
            </w:p>
          </w:ftr>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/header2.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.header+xml">
        <pkg:xmlData>
          <w:hdr mc:Ignorable="w14 wp14" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp14="http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:wpg="http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" xmlns:wpi="http://schemas.microsoft.com/office/word/2010/wordprocessingInk" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wps="http://schemas.microsoft.com/office/word/2010/wordprocessingShape">
            <w:p w:rsidR="00855765" w:rsidRPr="00855765" w:rsidRDefault="003E5AA7" w:rsidP="00855765">
              <w:pPr>
                <w:pStyle w:val="Header"/>
                <w:jc w:val="center"/>
                <w:rPr>
                  <w:sz w:val="28"/>
                </w:rPr>
              </w:pPr>
              <w:r>
                <w:rPr>
                  <w:sz w:val="28"/>
                </w:rPr>
                <w:t>APAS IMPORT-DATA</w:t>
              </w:r>
            </w:p>
          </w:hdr>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/header1.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.header+xml">
        <pkg:xmlData>
          <w:hdr mc:Ignorable="w14 wp14" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp14="http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:wpg="http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" xmlns:wpi="http://schemas.microsoft.com/office/word/2010/wordprocessingInk" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wps="http://schemas.microsoft.com/office/word/2010/wordprocessingShape">
            <w:p w:rsidR="00BE48CA" w:rsidRDefault="00BE48CA">
              <w:pPr>
                <w:pStyle w:val="Header"/>
              </w:pPr>
            </w:p>
          </w:hdr>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/endnotes.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.endnotes+xml">
        <pkg:xmlData>
          <w:endnotes mc:Ignorable="w14 wp14" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp14="http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:wpg="http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" xmlns:wpi="http://schemas.microsoft.com/office/word/2010/wordprocessingInk" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wps="http://schemas.microsoft.com/office/word/2010/wordprocessingShape">
            <w:endnote w:type="separator" w:id="-1">
              <w:p w:rsidR="00284D50" w:rsidRDefault="00284D50" w:rsidP="00855765">
                <w:pPr>
                  <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                </w:pPr>
                <w:r>
                  <w:separator/>
                </w:r>
              </w:p>
            </w:endnote>
            <w:endnote w:type="continuationSeparator" w:id="0">
              <w:p w:rsidR="00284D50" w:rsidRDefault="00284D50" w:rsidP="00855765">
                <w:pPr>
                  <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                </w:pPr>
                <w:r>
                  <w:continuationSeparator/>
                </w:r>
              </w:p>
            </w:endnote>
          </w:endnotes>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/footnotes.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.footnotes+xml">
        <pkg:xmlData>
          <w:footnotes mc:Ignorable="w14 wp14" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp14="http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:wpg="http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" xmlns:wpi="http://schemas.microsoft.com/office/word/2010/wordprocessingInk" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wps="http://schemas.microsoft.com/office/word/2010/wordprocessingShape">
            <w:footnote w:type="separator" w:id="-1">
              <w:p w:rsidR="00284D50" w:rsidRDefault="00284D50" w:rsidP="00855765">
                <w:pPr>
                  <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                </w:pPr>
                <w:r>
                  <w:separator/>
                </w:r>
              </w:p>
            </w:footnote>
            <w:footnote w:type="continuationSeparator" w:id="0">
              <w:p w:rsidR="00284D50" w:rsidRDefault="00284D50" w:rsidP="00855765">
                <w:pPr>
                  <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
                </w:pPr>
                <w:r>
                  <w:continuationSeparator/>
                </w:r>
              </w:p>
            </w:footnote>
          </w:footnotes>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/footer3.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.footer+xml">
        <pkg:xmlData>
          <w:ftr mc:Ignorable="w14 wp14" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:wp14="http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing" xmlns:wp="http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:wpg="http://schemas.microsoft.com/office/word/2010/wordprocessingGroup" xmlns:wpi="http://schemas.microsoft.com/office/word/2010/wordprocessingInk" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wps="http://schemas.microsoft.com/office/word/2010/wordprocessingShape">
            <w:p w:rsidR="00BE48CA" w:rsidRDefault="00BE48CA">
              <w:pPr>
                <w:pStyle w:val="Footer"/>
              </w:pPr>
            </w:p>
          </w:ftr>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/theme/theme1.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.theme+xml">
        <pkg:xmlData>
          <a:theme name="Office Theme" xmlns:a="http://schemas.openxmlformats.org/drawingml/2006/main">
            <a:themeElements>
              <a:clrScheme name="Office">
                <a:dk1>
                  <a:sysClr val="windowText" lastClr="000000"/>
                </a:dk1>
                <a:lt1>
                  <a:sysClr val="window" lastClr="FFFFFF"/>
                </a:lt1>
                <a:dk2>
                  <a:srgbClr val="1F497D"/>
                </a:dk2>
                <a:lt2>
                  <a:srgbClr val="EEECE1"/>
                </a:lt2>
                <a:accent1>
                  <a:srgbClr val="4F81BD"/>
                </a:accent1>
                <a:accent2>
                  <a:srgbClr val="C0504D"/>
                </a:accent2>
                <a:accent3>
                  <a:srgbClr val="9BBB59"/>
                </a:accent3>
                <a:accent4>
                  <a:srgbClr val="8064A2"/>
                </a:accent4>
                <a:accent5>
                  <a:srgbClr val="4BACC6"/>
                </a:accent5>
                <a:accent6>
                  <a:srgbClr val="F79646"/>
                </a:accent6>
                <a:hlink>
                  <a:srgbClr val="0000FF"/>
                </a:hlink>
                <a:folHlink>
                  <a:srgbClr val="800080"/>
                </a:folHlink>
              </a:clrScheme>
              <a:fontScheme name="Office">
                <a:majorFont>
                  <a:latin typeface="Cambria"/>
                  <a:ea typeface=""/>
                  <a:cs typeface=""/>
                  <a:font script="Jpan" typeface="ＭＳ ゴシック"/>
                  <a:font script="Hang" typeface="맑은 고딕"/>
                  <a:font script="Hans" typeface="宋体"/>
                  <a:font script="Hant" typeface="新細明體"/>
                  <a:font script="Arab" typeface="Times New Roman"/>
                  <a:font script="Hebr" typeface="Times New Roman"/>
                  <a:font script="Thai" typeface="Angsana New"/>
                  <a:font script="Ethi" typeface="Nyala"/>
                  <a:font script="Beng" typeface="Vrinda"/>
                  <a:font script="Gujr" typeface="Shruti"/>
                  <a:font script="Khmr" typeface="MoolBoran"/>
                  <a:font script="Knda" typeface="Tunga"/>
                  <a:font script="Guru" typeface="Raavi"/>
                  <a:font script="Cans" typeface="Euphemia"/>
                  <a:font script="Cher" typeface="Plantagenet Cherokee"/>
                  <a:font script="Yiii" typeface="Microsoft Yi Baiti"/>
                  <a:font script="Tibt" typeface="Microsoft Himalaya"/>
                  <a:font script="Thaa" typeface="MV Boli"/>
                  <a:font script="Deva" typeface="Mangal"/>
                  <a:font script="Telu" typeface="Gautami"/>
                  <a:font script="Taml" typeface="Latha"/>
                  <a:font script="Syrc" typeface="Estrangelo Edessa"/>
                  <a:font script="Orya" typeface="Kalinga"/>
                  <a:font script="Mlym" typeface="Kartika"/>
                  <a:font script="Laoo" typeface="DokChampa"/>
                  <a:font script="Sinh" typeface="Iskoola Pota"/>
                  <a:font script="Mong" typeface="Mongolian Baiti"/>
                  <a:font script="Viet" typeface="Times New Roman"/>
                  <a:font script="Uigh" typeface="Microsoft Uighur"/>
                  <a:font script="Geor" typeface="Sylfaen"/>
                </a:majorFont>
                <a:minorFont>
                  <a:latin typeface="Calibri"/>
                  <a:ea typeface=""/>
                  <a:cs typeface=""/>
                  <a:font script="Jpan" typeface="ＭＳ 明朝"/>
                  <a:font script="Hang" typeface="맑은 고딕"/>
                  <a:font script="Hans" typeface="宋体"/>
                  <a:font script="Hant" typeface="新細明體"/>
                  <a:font script="Arab" typeface="Arial"/>
                  <a:font script="Hebr" typeface="Arial"/>
                  <a:font script="Thai" typeface="Cordia New"/>
                  <a:font script="Ethi" typeface="Nyala"/>
                  <a:font script="Beng" typeface="Vrinda"/>
                  <a:font script="Gujr" typeface="Shruti"/>
                  <a:font script="Khmr" typeface="DaunPenh"/>
                  <a:font script="Knda" typeface="Tunga"/>
                  <a:font script="Guru" typeface="Raavi"/>
                  <a:font script="Cans" typeface="Euphemia"/>
                  <a:font script="Cher" typeface="Plantagenet Cherokee"/>
                  <a:font script="Yiii" typeface="Microsoft Yi Baiti"/>
                  <a:font script="Tibt" typeface="Microsoft Himalaya"/>
                  <a:font script="Thaa" typeface="MV Boli"/>
                  <a:font script="Deva" typeface="Mangal"/>
                  <a:font script="Telu" typeface="Gautami"/>
                  <a:font script="Taml" typeface="Latha"/>
                  <a:font script="Syrc" typeface="Estrangelo Edessa"/>
                  <a:font script="Orya" typeface="Kalinga"/>
                  <a:font script="Mlym" typeface="Kartika"/>
                  <a:font script="Laoo" typeface="DokChampa"/>
                  <a:font script="Sinh" typeface="Iskoola Pota"/>
                  <a:font script="Mong" typeface="Mongolian Baiti"/>
                  <a:font script="Viet" typeface="Arial"/>
                  <a:font script="Uigh" typeface="Microsoft Uighur"/>
                  <a:font script="Geor" typeface="Sylfaen"/>
                </a:minorFont>
              </a:fontScheme>
              <a:fmtScheme name="Office">
                <a:fillStyleLst>
                  <a:solidFill>
                    <a:schemeClr val="phClr"/>
                  </a:solidFill>
                  <a:gradFill rotWithShape="1">
                    <a:gsLst>
                      <a:gs pos="0">
                        <a:schemeClr val="phClr">
                          <a:tint val="50000"/>
                          <a:satMod val="300000"/>
                        </a:schemeClr>
                      </a:gs>
                      <a:gs pos="35000">
                        <a:schemeClr val="phClr">
                          <a:tint val="37000"/>
                          <a:satMod val="300000"/>
                        </a:schemeClr>
                      </a:gs>
                      <a:gs pos="100000">
                        <a:schemeClr val="phClr">
                          <a:tint val="15000"/>
                          <a:satMod val="350000"/>
                        </a:schemeClr>
                      </a:gs>
                    </a:gsLst>
                    <a:lin ang="16200000" scaled="1"/>
                  </a:gradFill>
                  <a:gradFill rotWithShape="1">
                    <a:gsLst>
                      <a:gs pos="0">
                        <a:schemeClr val="phClr">
                          <a:shade val="51000"/>
                          <a:satMod val="130000"/>
                        </a:schemeClr>
                      </a:gs>
                      <a:gs pos="80000">
                        <a:schemeClr val="phClr">
                          <a:shade val="93000"/>
                          <a:satMod val="130000"/>
                        </a:schemeClr>
                      </a:gs>
                      <a:gs pos="100000">
                        <a:schemeClr val="phClr">
                          <a:shade val="94000"/>
                          <a:satMod val="135000"/>
                        </a:schemeClr>
                      </a:gs>
                    </a:gsLst>
                    <a:lin ang="16200000" scaled="0"/>
                  </a:gradFill>
                </a:fillStyleLst>
                <a:lnStyleLst>
                  <a:ln w="9525" cap="flat" cmpd="sng" algn="ctr">
                    <a:solidFill>
                      <a:schemeClr val="phClr">
                        <a:shade val="95000"/>
                        <a:satMod val="105000"/>
                      </a:schemeClr>
                    </a:solidFill>
                    <a:prstDash val="solid"/>
                  </a:ln>
                  <a:ln w="25400" cap="flat" cmpd="sng" algn="ctr">
                    <a:solidFill>
                      <a:schemeClr val="phClr"/>
                    </a:solidFill>
                    <a:prstDash val="solid"/>
                  </a:ln>
                  <a:ln w="38100" cap="flat" cmpd="sng" algn="ctr">
                    <a:solidFill>
                      <a:schemeClr val="phClr"/>
                    </a:solidFill>
                    <a:prstDash val="solid"/>
                  </a:ln>
                </a:lnStyleLst>
                <a:effectStyleLst>
                  <a:effectStyle>
                    <a:effectLst>
                      <a:outerShdw blurRad="40000" dist="20000" dir="5400000" rotWithShape="0">
                        <a:srgbClr val="000000">
                          <a:alpha val="38000"/>
                        </a:srgbClr>
                      </a:outerShdw>
                    </a:effectLst>
                  </a:effectStyle>
                  <a:effectStyle>
                    <a:effectLst>
                      <a:outerShdw blurRad="40000" dist="23000" dir="5400000" rotWithShape="0">
                        <a:srgbClr val="000000">
                          <a:alpha val="35000"/>
                        </a:srgbClr>
                      </a:outerShdw>
                    </a:effectLst>
                  </a:effectStyle>
                  <a:effectStyle>
                    <a:effectLst>
                      <a:outerShdw blurRad="40000" dist="23000" dir="5400000" rotWithShape="0">
                        <a:srgbClr val="000000">
                          <a:alpha val="35000"/>
                        </a:srgbClr>
                      </a:outerShdw>
                    </a:effectLst>
                    <a:scene3d>
                      <a:camera prst="orthographicFront">
                        <a:rot lat="0" lon="0" rev="0"/>
                      </a:camera>
                      <a:lightRig rig="threePt" dir="t">
                        <a:rot lat="0" lon="0" rev="1200000"/>
                      </a:lightRig>
                    </a:scene3d>
                    <a:sp3d>
                      <a:bevelT w="63500" h="25400"/>
                    </a:sp3d>
                  </a:effectStyle>
                </a:effectStyleLst>
                <a:bgFillStyleLst>
                  <a:solidFill>
                    <a:schemeClr val="phClr"/>
                  </a:solidFill>
                  <a:gradFill rotWithShape="1">
                    <a:gsLst>
                      <a:gs pos="0">
                        <a:schemeClr val="phClr">
                          <a:tint val="40000"/>
                          <a:satMod val="350000"/>
                        </a:schemeClr>
                      </a:gs>
                      <a:gs pos="40000">
                        <a:schemeClr val="phClr">
                          <a:tint val="45000"/>
                          <a:shade val="99000"/>
                          <a:satMod val="350000"/>
                        </a:schemeClr>
                      </a:gs>
                      <a:gs pos="100000">
                        <a:schemeClr val="phClr">
                          <a:shade val="20000"/>
                          <a:satMod val="255000"/>
                        </a:schemeClr>
                      </a:gs>
                    </a:gsLst>
                    <a:path path="circle">
                      <a:fillToRect l="50000" t="-80000" r="50000" b="180000"/>
                    </a:path>
                  </a:gradFill>
                  <a:gradFill rotWithShape="1">
                    <a:gsLst>
                      <a:gs pos="0">
                        <a:schemeClr val="phClr">
                          <a:tint val="80000"/>
                          <a:satMod val="300000"/>
                        </a:schemeClr>
                      </a:gs>
                      <a:gs pos="100000">
                        <a:schemeClr val="phClr">
                          <a:shade val="30000"/>
                          <a:satMod val="200000"/>
                        </a:schemeClr>
                      </a:gs>
                    </a:gsLst>
                    <a:path path="circle">
                      <a:fillToRect l="50000" t="50000" r="50000" b="50000"/>
                    </a:path>
                  </a:gradFill>
                </a:bgFillStyleLst>
              </a:fmtScheme>
            </a:themeElements>
            <a:objectDefaults/>
            <a:extraClrSchemeLst/>
          </a:theme>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/_rels/settings.xml.rels" pkg:contentType="application/vnd.openxmlformats-package.relationships+xml">
        <pkg:xmlData>
          <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
            <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate" Target="file:///C:\Users\s0055804\Desktop\ExceptionReportTemplate.dotx" TargetMode="External"/>
          </Relationships>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/settings.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.settings+xml">
        <pkg:xmlData>
          <w:settings mc:Ignorable="w14" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:m="http://schemas.openxmlformats.org/officeDocument/2006/math" xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml" xmlns:sl="http://schemas.openxmlformats.org/schemaLibrary/2006/main">
            <w:zoom w:percent="120"/>
            <w:attachedTemplate r:id="rId1"/>
            <w:doNotTrackMoves/>
            <w:defaultTabStop w:val="720"/>
            <w:characterSpacingControl w:val="doNotCompress"/>
            <w:footnotePr>
              <w:footnote w:id="-1"/>
              <w:footnote w:id="0"/>
            </w:footnotePr>
            <w:endnotePr>
              <w:endnote w:id="-1"/>
              <w:endnote w:id="0"/>
            </w:endnotePr>
            <w:compat>
              <w:useNormalStyleForList/>
              <w:doNotUseIndentAsNumberingTabStop/>
              <w:useAltKinsokuLineBreakRules/>
              <w:allowSpaceOfSameStyleInTable/>
              <w:doNotSuppressIndentation/>
              <w:doNotAutofitConstrainedTables/>
              <w:autofitToFirstFixedWidthCell/>
              <w:displayHangulFixedWidth/>
              <w:splitPgBreakAndParaMark/>
              <w:doNotVertAlignCellWithSp/>
              <w:doNotBreakConstrainedForcedTable/>
              <w:doNotVertAlignInTxbx/>
              <w:useAnsiKerningPairs/>
              <w:cachedColBalance/>
              <w:compatSetting w:name="compatibilityMode" w:uri="http://schemas.microsoft.com/office/word" w:val="11"/>
            </w:compat>
            <w:rsids>
              <w:rsidRoot w:val="004423C8"/>
              <w:rsid w:val="00041C72"/>
              <w:rsid w:val="000514DD"/>
              <w:rsid w:val="00053BA3"/>
              <w:rsid w:val="00072B73"/>
              <w:rsid w:val="00077313"/>
              <w:rsid w:val="00080201"/>
              <w:rsid w:val="000A0B57"/>
              <w:rsid w:val="000B7F84"/>
              <w:rsid w:val="000D2467"/>
              <w:rsid w:val="000D5698"/>
              <w:rsid w:val="000F3D46"/>
              <w:rsid w:val="0019049F"/>
              <w:rsid w:val="001A096B"/>
              <w:rsid w:val="001E2A48"/>
              <w:rsid w:val="001E3BC1"/>
              <w:rsid w:val="002170C8"/>
              <w:rsid w:val="00220617"/>
              <w:rsid w:val="0026454D"/>
              <w:rsid w:val="0028328C"/>
              <w:rsid w:val="00284D50"/>
              <w:rsid w:val="00285479"/>
              <w:rsid w:val="00287178"/>
              <w:rsid w:val="002A7B2B"/>
              <w:rsid w:val="002F4A11"/>
              <w:rsid w:val="00342283"/>
              <w:rsid w:val="00344A67"/>
              <w:rsid w:val="00362C64"/>
              <w:rsid w:val="003810DD"/>
              <w:rsid w:val="00381A74"/>
              <w:rsid w:val="003901B4"/>
              <w:rsid w:val="003E2AB7"/>
              <w:rsid w:val="003E5AA7"/>
              <w:rsid w:val="00401998"/>
              <w:rsid w:val="004119EA"/>
              <w:rsid w:val="0042480E"/>
              <w:rsid w:val="004423C8"/>
              <w:rsid w:val="004F3ADC"/>
              <w:rsid w:val="00506AC4"/>
              <w:rsid w:val="00521D59"/>
              <w:rsid w:val="00521DA8"/>
              <w:rsid w:val="00531A54"/>
              <w:rsid w:val="005C6E85"/>
              <w:rsid w:val="006002E7"/>
              <w:rsid w:val="00627065"/>
              <w:rsid w:val="00644EF0"/>
              <w:rsid w:val="007443D2"/>
              <w:rsid w:val="007676CE"/>
              <w:rsid w:val="00797987"/>
              <w:rsid w:val="007B4C01"/>
              <w:rsid w:val="007C134A"/>
              <w:rsid w:val="007C1AC2"/>
              <w:rsid w:val="007C30E4"/>
              <w:rsid w:val="007D00FA"/>
              <w:rsid w:val="007D59E8"/>
              <w:rsid w:val="0082093C"/>
              <w:rsid w:val="00841434"/>
              <w:rsid w:val="0085199D"/>
              <w:rsid w:val="00855765"/>
              <w:rsid w:val="008569D0"/>
              <w:rsid w:val="00872426"/>
              <w:rsid w:val="008775DB"/>
              <w:rsid w:val="00881E56"/>
              <w:rsid w:val="008F13A5"/>
              <w:rsid w:val="0093031D"/>
              <w:rsid w:val="00955FFC"/>
              <w:rsid w:val="00A10A76"/>
              <w:rsid w:val="00A25BB3"/>
              <w:rsid w:val="00A5009D"/>
              <w:rsid w:val="00A8027D"/>
              <w:rsid w:val="00A83114"/>
              <w:rsid w:val="00AF6C37"/>
              <w:rsid w:val="00B075BF"/>
              <w:rsid w:val="00B5417A"/>
              <w:rsid w:val="00B70ECA"/>
              <w:rsid w:val="00BA05DD"/>
              <w:rsid w:val="00BA603D"/>
              <w:rsid w:val="00BB45A3"/>
              <w:rsid w:val="00BE48CA"/>
              <w:rsid w:val="00C065D8"/>
              <w:rsid w:val="00C47D93"/>
              <w:rsid w:val="00C85972"/>
              <w:rsid w:val="00CA59F9"/>
              <w:rsid w:val="00D650CF"/>
              <w:rsid w:val="00D973AA"/>
              <w:rsid w:val="00DB2600"/>
              <w:rsid w:val="00DC6661"/>
              <w:rsid w:val="00E24434"/>
              <w:rsid w:val="00E3214B"/>
              <w:rsid w:val="00E35B5F"/>
              <w:rsid w:val="00E40F4F"/>
              <w:rsid w:val="00E4281B"/>
              <w:rsid w:val="00E62AF0"/>
              <w:rsid w:val="00E84AB8"/>
              <w:rsid w:val="00E852A3"/>
              <w:rsid w:val="00EB34FF"/>
              <w:rsid w:val="00F23408"/>
              <w:rsid w:val="00F37A98"/>
              <w:rsid w:val="00FA1F29"/>
              <w:rsid w:val="00FA30CA"/>
              <w:rsid w:val="00FA49FF"/>
              <w:rsid w:val="00FA5A5E"/>
              <w:rsid w:val="00FD752A"/>
            </w:rsids>
            <m:mathPr>
              <m:mathFont m:val="Cambria Math"/>
              <m:brkBin m:val="before"/>
              <m:brkBinSub m:val="--"/>
              <m:smallFrac m:val="0"/>
              <m:dispDef/>
              <m:lMargin m:val="0"/>
              <m:rMargin m:val="0"/>
              <m:defJc m:val="centerGroup"/>
              <m:wrapIndent m:val="1440"/>
              <m:intLim m:val="subSup"/>
              <m:naryLim m:val="undOvr"/>
            </m:mathPr>
            <w:themeFontLang w:val="en-US"/>
            <w:clrSchemeMapping w:bg1="light1" w:t1="dark1" w:bg2="light2" w:t2="dark2" w:accent1="accent1" w:accent2="accent2" w:accent3="accent3" w:accent4="accent4" w:accent5="accent5" w:accent6="accent6" w:hyperlink="hyperlink" w:followedHyperlink="followedHyperlink"/>
            <w:shapeDefaults>
              <o:shapedefaults v:ext="edit" spidmax="1026"/>
              <o:shapelayout v:ext="edit">
                <o:idmap v:ext="edit" data="1"/>
              </o:shapelayout>
            </w:shapeDefaults>
            <w:decimalSymbol w:val="."/>
            <w:listSeparator w:val=","/>
          </w:settings>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/fontTable.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.fontTable+xml">
        <pkg:xmlData>
          <w:fonts mc:Ignorable="w14" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml">
            <w:font w:name="Calibri">
              <w:panose1 w:val="020F0502020204030204"/>
              <w:charset w:val="00"/>
              <w:family w:val="swiss"/>
              <w:pitch w:val="variable"/>
              <w:sig w:usb0="E00002FF" w:usb1="4000ACFF" w:usb2="00000001" w:usb3="00000000" w:csb0="0000019F" w:csb1="00000000"/>
            </w:font>
            <w:font w:name="Times New Roman">
              <w:panose1 w:val="02020603050405020304"/>
              <w:charset w:val="00"/>
              <w:family w:val="roman"/>
              <w:pitch w:val="variable"/>
              <w:sig w:usb0="E0002AFF" w:usb1="C0007841" w:usb2="00000009" w:usb3="00000000" w:csb0="000001FF" w:csb1="00000000"/>
            </w:font>
            <w:font w:name="Tahoma">
              <w:panose1 w:val="020B0604030504040204"/>
              <w:charset w:val="00"/>
              <w:family w:val="swiss"/>
              <w:pitch w:val="variable"/>
              <w:sig w:usb0="E1002EFF" w:usb1="C000605B" w:usb2="00000029" w:usb3="00000000" w:csb0="000101FF" w:csb1="00000000"/>
            </w:font>
            <w:font w:name="Cambria">
              <w:panose1 w:val="02040503050406030204"/>
              <w:charset w:val="00"/>
              <w:family w:val="roman"/>
              <w:pitch w:val="variable"/>
              <w:sig w:usb0="E00002FF" w:usb1="400004FF" w:usb2="00000000" w:usb3="00000000" w:csb0="0000019F" w:csb1="00000000"/>
            </w:font>
          </w:fonts>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/webSettings.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.webSettings+xml">
        <pkg:xmlData>
          <w:webSettings mc:Ignorable="w14" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml">
            <w:optimizeForBrowser/>
            <w:relyOnVML/>
            <w:allowPNG/>
          </w:webSettings>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/customXml/_rels/item1.xml.rels" pkg:contentType="application/vnd.openxmlformats-package.relationships+xml" pkg:padding="256">
        <pkg:xmlData>
          <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
            <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/customXmlProps" Target="itemProps1.xml"/>
          </Relationships>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/customXml/itemProps1.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.customXmlProperties+xml" pkg:padding="32">
        <pkg:xmlData pkg:originalXmlStandalone="no">
          <ds:datastoreItem ds:itemID="{D0EB451A-4478-404C-8F67-6F51F7B294BB}" xmlns:ds="http://schemas.openxmlformats.org/officeDocument/2006/customXml">
            <ds:schemaRefs>
              <ds:schemaRef ds:uri="http://schemas.openxmlformats.org/officeDocument/2006/bibliography"/>
            </ds:schemaRefs>
          </ds:datastoreItem>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/word/styles.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.wordprocessingml.styles+xml">
        <pkg:xmlData>
          <w:styles mc:Ignorable="w14" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships" xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:w14="http://schemas.microsoft.com/office/word/2010/wordml">
            <w:docDefaults>
              <w:rPrDefault>
                <w:rPr>
                  <w:rFonts w:ascii="Calibri" w:eastAsia="Calibri" w:hAnsi="Calibri" w:cs="Times New Roman"/>
                  <w:lang w:val="en-US" w:eastAsia="en-US" w:bidi="ar-SA"/>
                </w:rPr>
              </w:rPrDefault>
              <w:pPrDefault/>
            </w:docDefaults>
            <w:latentStyles w:defLockedState="0" w:defUIPriority="99" w:defSemiHidden="1" w:defUnhideWhenUsed="1" w:defQFormat="0" w:count="267">
              <w:lsdException w:name="Normal" w:semiHidden="0" w:uiPriority="0" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="heading 1" w:semiHidden="0" w:uiPriority="9" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="heading 2" w:uiPriority="9" w:qFormat="1"/>
              <w:lsdException w:name="heading 3" w:uiPriority="9" w:qFormat="1"/>
              <w:lsdException w:name="heading 4" w:uiPriority="9" w:qFormat="1"/>
              <w:lsdException w:name="heading 5" w:uiPriority="9" w:qFormat="1"/>
              <w:lsdException w:name="heading 6" w:uiPriority="9" w:qFormat="1"/>
              <w:lsdException w:name="heading 7" w:uiPriority="9" w:qFormat="1"/>
              <w:lsdException w:name="heading 8" w:uiPriority="9" w:qFormat="1"/>
              <w:lsdException w:name="heading 9" w:uiPriority="9" w:qFormat="1"/>
              <w:lsdException w:name="toc 1" w:uiPriority="39"/>
              <w:lsdException w:name="toc 2" w:uiPriority="39"/>
              <w:lsdException w:name="toc 3" w:uiPriority="39"/>
              <w:lsdException w:name="toc 4" w:uiPriority="39"/>
              <w:lsdException w:name="toc 5" w:uiPriority="39"/>
              <w:lsdException w:name="toc 6" w:uiPriority="39"/>
              <w:lsdException w:name="toc 7" w:uiPriority="39"/>
              <w:lsdException w:name="toc 8" w:uiPriority="39"/>
              <w:lsdException w:name="toc 9" w:uiPriority="39"/>
              <w:lsdException w:name="caption" w:uiPriority="35" w:qFormat="1"/>
              <w:lsdException w:name="Title" w:semiHidden="0" w:uiPriority="10" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Default Paragraph Font" w:uiPriority="1"/>
              <w:lsdException w:name="Subtitle" w:semiHidden="0" w:uiPriority="11" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Strong" w:semiHidden="0" w:uiPriority="22" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Emphasis" w:semiHidden="0" w:uiPriority="20" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Table Grid" w:semiHidden="0" w:uiPriority="59" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Placeholder Text" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="No Spacing" w:semiHidden="0" w:uiPriority="1" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Light Shading" w:semiHidden="0" w:uiPriority="60" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light List" w:semiHidden="0" w:uiPriority="61" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Grid" w:semiHidden="0" w:uiPriority="62" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 1" w:semiHidden="0" w:uiPriority="63" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 2" w:semiHidden="0" w:uiPriority="64" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 1" w:semiHidden="0" w:uiPriority="65" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 2" w:semiHidden="0" w:uiPriority="66" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 1" w:semiHidden="0" w:uiPriority="67" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 2" w:semiHidden="0" w:uiPriority="68" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 3" w:semiHidden="0" w:uiPriority="69" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Dark List" w:semiHidden="0" w:uiPriority="70" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Shading" w:semiHidden="0" w:uiPriority="71" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful List" w:semiHidden="0" w:uiPriority="72" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Grid" w:semiHidden="0" w:uiPriority="73" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Shading Accent 1" w:semiHidden="0" w:uiPriority="60" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light List Accent 1" w:semiHidden="0" w:uiPriority="61" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Grid Accent 1" w:semiHidden="0" w:uiPriority="62" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 1 Accent 1" w:semiHidden="0" w:uiPriority="63" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 2 Accent 1" w:semiHidden="0" w:uiPriority="64" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 1 Accent 1" w:semiHidden="0" w:uiPriority="65" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Revision" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="List Paragraph" w:semiHidden="0" w:uiPriority="34" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Quote" w:semiHidden="0" w:uiPriority="29" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Intense Quote" w:semiHidden="0" w:uiPriority="30" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Medium List 2 Accent 1" w:semiHidden="0" w:uiPriority="66" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 1 Accent 1" w:semiHidden="0" w:uiPriority="67" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 2 Accent 1" w:semiHidden="0" w:uiPriority="68" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 3 Accent 1" w:semiHidden="0" w:uiPriority="69" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Dark List Accent 1" w:semiHidden="0" w:uiPriority="70" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Shading Accent 1" w:semiHidden="0" w:uiPriority="71" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful List Accent 1" w:semiHidden="0" w:uiPriority="72" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Grid Accent 1" w:semiHidden="0" w:uiPriority="73" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Shading Accent 2" w:semiHidden="0" w:uiPriority="60" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light List Accent 2" w:semiHidden="0" w:uiPriority="61" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Grid Accent 2" w:semiHidden="0" w:uiPriority="62" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 1 Accent 2" w:semiHidden="0" w:uiPriority="63" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 2 Accent 2" w:semiHidden="0" w:uiPriority="64" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 1 Accent 2" w:semiHidden="0" w:uiPriority="65" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 2 Accent 2" w:semiHidden="0" w:uiPriority="66" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 1 Accent 2" w:semiHidden="0" w:uiPriority="67" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 2 Accent 2" w:semiHidden="0" w:uiPriority="68" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 3 Accent 2" w:semiHidden="0" w:uiPriority="69" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Dark List Accent 2" w:semiHidden="0" w:uiPriority="70" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Shading Accent 2" w:semiHidden="0" w:uiPriority="71" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful List Accent 2" w:semiHidden="0" w:uiPriority="72" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Grid Accent 2" w:semiHidden="0" w:uiPriority="73" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Shading Accent 3" w:semiHidden="0" w:uiPriority="60" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light List Accent 3" w:semiHidden="0" w:uiPriority="61" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Grid Accent 3" w:semiHidden="0" w:uiPriority="62" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 1 Accent 3" w:semiHidden="0" w:uiPriority="63" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 2 Accent 3" w:semiHidden="0" w:uiPriority="64" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 1 Accent 3" w:semiHidden="0" w:uiPriority="65" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 2 Accent 3" w:semiHidden="0" w:uiPriority="66" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 1 Accent 3" w:semiHidden="0" w:uiPriority="67" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 2 Accent 3" w:semiHidden="0" w:uiPriority="68" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 3 Accent 3" w:semiHidden="0" w:uiPriority="69" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Dark List Accent 3" w:semiHidden="0" w:uiPriority="70" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Shading Accent 3" w:semiHidden="0" w:uiPriority="71" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful List Accent 3" w:semiHidden="0" w:uiPriority="72" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Grid Accent 3" w:semiHidden="0" w:uiPriority="73" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Shading Accent 4" w:semiHidden="0" w:uiPriority="60" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light List Accent 4" w:semiHidden="0" w:uiPriority="61" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Grid Accent 4" w:semiHidden="0" w:uiPriority="62" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 1 Accent 4" w:semiHidden="0" w:uiPriority="63" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 2 Accent 4" w:semiHidden="0" w:uiPriority="64" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 1 Accent 4" w:semiHidden="0" w:uiPriority="65" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 2 Accent 4" w:semiHidden="0" w:uiPriority="66" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 1 Accent 4" w:semiHidden="0" w:uiPriority="67" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 2 Accent 4" w:semiHidden="0" w:uiPriority="68" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 3 Accent 4" w:semiHidden="0" w:uiPriority="69" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Dark List Accent 4" w:semiHidden="0" w:uiPriority="70" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Shading Accent 4" w:semiHidden="0" w:uiPriority="71" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful List Accent 4" w:semiHidden="0" w:uiPriority="72" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Grid Accent 4" w:semiHidden="0" w:uiPriority="73" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Shading Accent 5" w:semiHidden="0" w:uiPriority="60" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light List Accent 5" w:semiHidden="0" w:uiPriority="61" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Grid Accent 5" w:semiHidden="0" w:uiPriority="62" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 1 Accent 5" w:semiHidden="0" w:uiPriority="63" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 2 Accent 5" w:semiHidden="0" w:uiPriority="64" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 1 Accent 5" w:semiHidden="0" w:uiPriority="65" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 2 Accent 5" w:semiHidden="0" w:uiPriority="66" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 1 Accent 5" w:semiHidden="0" w:uiPriority="67" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 2 Accent 5" w:semiHidden="0" w:uiPriority="68" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 3 Accent 5" w:semiHidden="0" w:uiPriority="69" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Dark List Accent 5" w:semiHidden="0" w:uiPriority="70" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Shading Accent 5" w:semiHidden="0" w:uiPriority="71" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful List Accent 5" w:semiHidden="0" w:uiPriority="72" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Grid Accent 5" w:semiHidden="0" w:uiPriority="73" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Shading Accent 6" w:semiHidden="0" w:uiPriority="60" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light List Accent 6" w:semiHidden="0" w:uiPriority="61" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Light Grid Accent 6" w:semiHidden="0" w:uiPriority="62" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 1 Accent 6" w:semiHidden="0" w:uiPriority="63" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Shading 2 Accent 6" w:semiHidden="0" w:uiPriority="64" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 1 Accent 6" w:semiHidden="0" w:uiPriority="65" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium List 2 Accent 6" w:semiHidden="0" w:uiPriority="66" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 1 Accent 6" w:semiHidden="0" w:uiPriority="67" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 2 Accent 6" w:semiHidden="0" w:uiPriority="68" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Medium Grid 3 Accent 6" w:semiHidden="0" w:uiPriority="69" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Dark List Accent 6" w:semiHidden="0" w:uiPriority="70" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Shading Accent 6" w:semiHidden="0" w:uiPriority="71" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful List Accent 6" w:semiHidden="0" w:uiPriority="72" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Colorful Grid Accent 6" w:semiHidden="0" w:uiPriority="73" w:unhideWhenUsed="0"/>
              <w:lsdException w:name="Subtle Emphasis" w:semiHidden="0" w:uiPriority="19" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Intense Emphasis" w:semiHidden="0" w:uiPriority="21" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Subtle Reference" w:semiHidden="0" w:uiPriority="31" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Intense Reference" w:semiHidden="0" w:uiPriority="32" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Book Title" w:semiHidden="0" w:uiPriority="33" w:unhideWhenUsed="0" w:qFormat="1"/>
              <w:lsdException w:name="Bibliography" w:uiPriority="37"/>
              <w:lsdException w:name="TOC Heading" w:uiPriority="39" w:qFormat="1"/>
            </w:latentStyles>
            <w:style w:type="paragraph" w:default="1" w:styleId="Normal">
              <w:name w:val="Normal"/>
              <w:qFormat/>
              <w:pPr>
                <w:spacing w:after="200" w:line="276" w:lineRule="auto"/>
              </w:pPr>
              <w:rPr>
                <w:sz w:val="22"/>
                <w:szCs w:val="22"/>
              </w:rPr>
            </w:style>
            <w:style w:type="character" w:default="1" w:styleId="DefaultParagraphFont">
              <w:name w:val="Default Paragraph Font"/>
              <w:uiPriority w:val="1"/>
              <w:semiHidden/>
              <w:unhideWhenUsed/>
            </w:style>
            <w:style w:type="table" w:default="1" w:styleId="TableNormal">
              <w:name w:val="Normal Table"/>
              <w:uiPriority w:val="99"/>
              <w:semiHidden/>
              <w:unhideWhenUsed/>
              <w:tblPr>
                <w:tblInd w:w="0" w:type="dxa"/>
                <w:tblCellMar>
                  <w:top w:w="0" w:type="dxa"/>
                  <w:left w:w="108" w:type="dxa"/>
                  <w:bottom w:w="0" w:type="dxa"/>
                  <w:right w:w="108" w:type="dxa"/>
                </w:tblCellMar>
              </w:tblPr>
            </w:style>
            <w:style w:type="numbering" w:default="1" w:styleId="NoList">
              <w:name w:val="No List"/>
              <w:uiPriority w:val="99"/>
              <w:semiHidden/>
              <w:unhideWhenUsed/>
            </w:style>
            <w:style w:type="paragraph" w:styleId="Header">
              <w:name w:val="header"/>
              <w:basedOn w:val="Normal"/>
              <w:link w:val="HeaderChar"/>
              <w:uiPriority w:val="99"/>
              <w:unhideWhenUsed/>
              <w:rsid w:val="00855765"/>
              <w:pPr>
                <w:tabs>
                  <w:tab w:val="center" w:pos="4680"/>
                  <w:tab w:val="right" w:pos="9360"/>
                </w:tabs>
                <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
              </w:pPr>
            </w:style>
            <w:style w:type="character" w:customStyle="1" w:styleId="HeaderChar">
              <w:name w:val="Header Char"/>
              <w:basedOn w:val="DefaultParagraphFont"/>
              <w:link w:val="Header"/>
              <w:uiPriority w:val="99"/>
              <w:rsid w:val="00855765"/>
            </w:style>
            <w:style w:type="paragraph" w:styleId="Footer">
              <w:name w:val="footer"/>
              <w:basedOn w:val="Normal"/>
              <w:link w:val="FooterChar"/>
              <w:uiPriority w:val="99"/>
              <w:unhideWhenUsed/>
              <w:rsid w:val="00855765"/>
              <w:pPr>
                <w:tabs>
                  <w:tab w:val="center" w:pos="4680"/>
                  <w:tab w:val="right" w:pos="9360"/>
                </w:tabs>
                <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
              </w:pPr>
            </w:style>
            <w:style w:type="character" w:customStyle="1" w:styleId="FooterChar">
              <w:name w:val="Footer Char"/>
              <w:basedOn w:val="DefaultParagraphFont"/>
              <w:link w:val="Footer"/>
              <w:uiPriority w:val="99"/>
              <w:rsid w:val="00855765"/>
            </w:style>
            <w:style w:type="paragraph" w:styleId="BalloonText">
              <w:name w:val="Balloon Text"/>
              <w:basedOn w:val="Normal"/>
              <w:link w:val="BalloonTextChar"/>
              <w:uiPriority w:val="99"/>
              <w:semiHidden/>
              <w:unhideWhenUsed/>
              <w:rsid w:val="00855765"/>
              <w:pPr>
                <w:spacing w:after="0" w:line="240" w:lineRule="auto"/>
              </w:pPr>
              <w:rPr>
                <w:rFonts w:ascii="Tahoma" w:hAnsi="Tahoma" w:cs="Tahoma"/>
                <w:sz w:val="16"/>
                <w:szCs w:val="16"/>
              </w:rPr>
            </w:style>
            <w:style w:type="character" w:customStyle="1" w:styleId="BalloonTextChar">
              <w:name w:val="Balloon Text Char"/>
              <w:link w:val="BalloonText"/>
              <w:uiPriority w:val="99"/>
              <w:semiHidden/>
              <w:rsid w:val="00855765"/>
              <w:rPr>
                <w:rFonts w:ascii="Tahoma" w:hAnsi="Tahoma" w:cs="Tahoma"/>
                <w:sz w:val="16"/>
                <w:szCs w:val="16"/>
              </w:rPr>
            </w:style>
            <w:style w:type="table" w:styleId="TableGrid">
              <w:name w:val="Table Grid"/>
              <w:basedOn w:val="TableNormal"/>
              <w:uiPriority w:val="59"/>
              <w:rsid w:val="00855765"/>
              <w:tblPr>
                <w:tblBorders>
                  <w:top w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                  <w:left w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                  <w:bottom w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                  <w:right w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                  <w:insideH w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                  <w:insideV w:val="single" w:sz="4" w:space="0" w:color="auto"/>
                </w:tblBorders>
              </w:tblPr>
            </w:style>
          </w:styles>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/docProps/app.xml" pkg:contentType="application/vnd.openxmlformats-officedocument.extended-properties+xml" pkg:padding="256">
        <pkg:xmlData>
          <Properties xmlns="http://schemas.openxmlformats.org/officeDocument/2006/extended-properties" xmlns:vt="http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes">
            <Template>ExceptionReportTemplate.dotx</Template>
            <TotalTime>0</TotalTime>
            <Pages>2</Pages>
            <Words>549</Words>
            <Characters>3133</Characters>
            <Application>Microsoft Office Word</Application>
            <DocSecurity>0</DocSecurity>
            <Lines>26</Lines>
            <Paragraphs>7</Paragraphs>
            <ScaleCrop>false</ScaleCrop>
            <HeadingPairs>
              <vt:vector size="2" baseType="variant">
                <vt:variant>
                  <vt:lpstr>Title</vt:lpstr>
                </vt:variant>
                <vt:variant>
                  <vt:i4>1</vt:i4>
                </vt:variant>
              </vt:vector>
            </HeadingPairs>
            <TitlesOfParts>
              <vt:vector size="1" baseType="lpstr">
                <vt:lpstr/>
              </vt:vector>
            </TitlesOfParts>
            <Company>Lethbridge College</Company>
            <LinksUpToDate>false</LinksUpToDate>
            <CharactersWithSpaces>3675</CharactersWithSpaces>
            <SharedDoc>false</SharedDoc>
            <HyperlinksChanged>false</HyperlinksChanged>
            <AppVersion>14.0000</AppVersion>
          </Properties>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/docProps/core.xml" pkg:contentType="application/vnd.openxmlformats-package.core-properties+xml" pkg:padding="256">
        <pkg:xmlData>
          <cp:coreProperties xmlns:cp="http://schemas.openxmlformats.org/package/2006/metadata/core-properties" xmlns:dc="http://purl.org/dc/elements/1.1/" xmlns:dcterms="http://purl.org/dc/terms/" xmlns:dcmitype="http://purl.org/dc/dcmitype/" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
            <dc:creator>Patrick Dudley (s0055804)</dc:creator>
            <cp:lastModifiedBy>Patrick Dudley (s0055804)</cp:lastModifiedBy>
            <cp:revision>3</cp:revision>
            <cp:lastPrinted>2014-10-16T15:52:00Z</cp:lastPrinted>
            <dcterms:created xsi:type="dcterms:W3CDTF">2014-10-17T20:23:00Z</dcterms:created>
            <dcterms:modified xsi:type="dcterms:W3CDTF">2014-10-17T20:23:00Z</dcterms:modified>
          </cp:coreProperties>
        </pkg:xmlData>
      </pkg:part>
      <pkg:part pkg:name="/customXml/item1.xml" pkg:contentType="application/xml" pkg:padding="32">
        <pkg:xmlData>
          <b:Sources SelectedStyle="\APA.XSL" StyleName="APA" xmlns:b="http://schemas.openxmlformats.org/officeDocument/2006/bibliography" xmlns="http://schemas.openxmlformats.org/officeDocument/2006/bibliography"/>
        </pkg:xmlData>
      </pkg:part>
    </pkg:package>
  </xsl:template>
</xsl:stylesheet>
