<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template name="formatdate">
    <xsl:param name="dateParam" />
    <!-- input format xslt datetime string -->
    <!-- output format mm/dd/yyyy hh:mm -->

    <xsl:variable name="datestr">
      <xsl:value-of select="substring-before($dateParam,'T')" />
    </xsl:variable>

    <xsl:choose>
      <xsl:when test="$datestr = ''">
        <xsl:value-of select="$datestr" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:variable name="mm">
          <xsl:value-of select="substring($datestr,6,2)" />
        </xsl:variable>
        <xsl:variable name="dd">
          <xsl:value-of select="substring($datestr,9,2)" />
        </xsl:variable>
        <xsl:variable name="yyyy">
          <xsl:value-of select="substring($datestr,1,4)" />
        </xsl:variable>

        <xsl:variable name="timestr">
          <xsl:value-of select="substring-after($dateParam,'T')" />
        </xsl:variable>
        <xsl:variable name="hh">
          <xsl:value-of select="substring($timestr,1,2)" />
        </xsl:variable>
        <xsl:variable name="min">
          <xsl:value-of select="substring($timestr,4,2)" />
        </xsl:variable>

        <xsl:value-of select="concat($dd,'/', $mm, '/', $yyyy, ' ', $hh, ':', $min)" />
      </xsl:otherwise>
    </xsl:choose>

  </xsl:template>
  <xsl:template match="/">
    <?mso-application progid="Excel.Sheet"?>
    <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
		 xmlns:o="urn:schemas-microsoft-com:office:office"
		 xmlns:x="urn:schemas-microsoft-com:office:excel"
		 xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
		 xmlns:html="http://www.w3.org/TR/REC-html40">
      <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
        <Author>Patrick Dudley</Author>
        <LastAuthor>Patrick Dudley</LastAuthor>
        <Created></Created>
        <Version>14.00</Version>
      </DocumentProperties>
      <OfficeDocumentSettings xmlns="urn:schemas-microsoft-com:office:office">
        <AllowPNG/>
      </OfficeDocumentSettings>
      <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
        <WindowHeight>12330</WindowHeight>
        <WindowWidth>27795</WindowWidth>
        <WindowTopX>480</WindowTopX>
        <WindowTopY>90</WindowTopY>
        <ProtectStructure>False</ProtectStructure>
        <ProtectWindows>False</ProtectWindows>
      </ExcelWorkbook>
      <Styles>
        <Style ss:ID="Default" ss:Name="Normal">
          <Alignment ss:Vertical="Bottom"/>
          <Borders/>
          <Font ss:FontName="Calibri" x:Family="Swiss" ss:Size="11" ss:Color="#000000"/>
          <Interior/>
          <NumberFormat/>
          <Protection/>
        </Style>
        <Style ss:ID="s63">
          <Alignment ss:Horizontal="Left" ss:Vertical="Bottom"/>
        </Style>
        <Style ss:ID="s64">
          <Alignment ss:Horizontal="Left" ss:Vertical="Bottom"/>
          <NumberFormat ss:Format="General Date"/>
        </Style>
        <Style ss:ID="s73">
          <Alignment ss:Horizontal="Left" ss:Vertical="Bottom"/>
          <Font ss:FontName="Calibri" x:Family="Swiss" ss:Size="11" ss:Color="#0F243E"/>
          <Interior ss:Color="#C5D9F1" ss:Pattern="Solid"/>
        </Style>
        <Style ss:ID="s82">
          <Alignment ss:Horizontal="Left" ss:Vertical="Center"/>
          <Font ss:FontName="Calibri" x:Family="Swiss" ss:Size="18" ss:Color="#000000"/>
        </Style>
        <Style ss:ID="s91">
          <Alignment ss:Horizontal="Right" ss:Vertical="Center"/>
          <Font ss:FontName="Calibri" x:Family="Swiss" ss:Size="18" ss:Color="#000000"/>
          <NumberFormat ss:Format="General Date"/>
        </Style>
      </Styles>
      <Worksheet ss:Name="ApasImportReport">
        <Names>
          <NamedRange ss:Name="_FilterDatabase" ss:RefersTo="=ApasImportReport!R3C1:R3C11" ss:Hidden="1"/>
        </Names>
        
        <Table ss:StyleID="s63" ss:DefaultRowHeight="15">
          <Column ss:StyleID="s63" ss:Width="83.25"/>
          <Column ss:StyleID="s63" ss:Width="52.5"/>
          <Column ss:StyleID="s63" ss:Width="52.5"/>
          <Column ss:StyleID="s63" ss:Width="100"/>
          <Column ss:StyleID="s63" ss:Width="100"/>
          <Column ss:StyleID="s63" ss:Width="100"/>
          <Column ss:StyleID="s63" ss:Width="150"/>
          <Column ss:StyleID="s63" ss:Width="65"/>
          <Column ss:StyleID="s63" ss:Width="82"/>
          <Column ss:StyleID="s63" ss:Width="100"/>
          <Column ss:StyleID="s63" ss:Width="85"/>
          <Row>
            <Cell ss:MergeAcross="8" ss:MergeDown="1" ss:StyleID="s82">
              <Data ss:Type="String">APAS Admission Applications Import Summary Report</Data>
            </Cell>
            <Cell ss:MergeAcross="1" ss:MergeDown="1" ss:StyleID="s91" ss:Formula="=NOW()">
              <Data ss:Type="DateTime">2016-06-22T13:03:46.470</Data>
            </Cell>
          </Row>
          <Row ss:Index="3">
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">Submitted</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">ASN</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">sNumber</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">Last Name</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">First Name</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">Middle Name</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">Email</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">Gender</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">AdmitStatus</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">Program</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">Term</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
            <Cell ss:StyleID="s73">
              <Data ss:Type="String">Exported</Data>
              <NamedCell ss:Name="_FilterDatabase"/>
            </Cell>
          </Row>
          <xsl:for-each select="ArrayOfApplicationSchemaFilter/ApplicationSchemaFilter">
            <Row>
              <Cell ss:StyleID="s64">
                <Data ss:Type="String">
                  <xsl:call-template name="formatdate">
                    <xsl:with-param name="dateParam" select="./datesubmitted"/>
                  </xsl:call-template>

                  <!--<xsl:value-of select="./datesubmitted"/>-->
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="Number">
                  <xsl:value-of select="./AlbertaEducationNumber"/>
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="String">
                  <xsl:value-of select="./previousStudentNumber"/>
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="String">
                  <xsl:value-of select="./lastName"/>
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="String">
                  <xsl:value-of select="./firstName"/>
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="String">
                  <xsl:value-of select="./middleName"/>
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="String">
                  <xsl:value-of select="./email"/>
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="String">
                  <xsl:value-of select="./sexID"/>
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="Number">
                  <xsl:value-of select="./StartYear"/>
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="String">
                  <xsl:value-of select="./Program"/>
                </Data>
              </Cell>
              <Cell>
                <Data ss:Type="String">
                  <xsl:value-of select="./entryTerm"/>
                </Data>
              </Cell>
              <Cell ss:StyleID="s64">
                <Data ss:Type="String">
                  <xsl:value-of select="./Exported"/>
                </Data>
              </Cell>
            </Row>
          </xsl:for-each>
        </Table>
        
        <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
          <PageSetup>
            <Header x:Margin="0.3"/>
            <Footer x:Margin="0.3"/>
            <PageMargins x:Bottom="0.75" x:Left="0.7" x:Right="0.7" x:Top="0.75"/>
          </PageSetup>
          <Selected/>
          <Panes>
            <Pane>
              <Number>1</Number>
              <ActiveRow>7</ActiveRow>
              <ActiveCol>5</ActiveCol>
            </Pane>
          </Panes>
          <ProtectObjects>False</ProtectObjects>
          <ProtectScenarios>False</ProtectScenarios>
        </WorksheetOptions>
        <AutoFilter x:Range="R3C1:R3C11" xmlns="urn:schemas-microsoft-com:office:excel">
        </AutoFilter>
      </Worksheet>
    </Workbook>
  </xsl:template>
</xsl:stylesheet>