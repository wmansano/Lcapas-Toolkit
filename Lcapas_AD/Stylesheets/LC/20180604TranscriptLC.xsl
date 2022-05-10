<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:output method="html"/>

  <xsl:template match="/">
    <html>
      <head>
        <xsl:call-template name="htmlHead" />
      </head>
      <body style="font-size:13px; width:auto; ">
        <xsl:call-template name="documentHeader" />
        <xsl:call-template name="transcriptDetailsGrid" />
        <xsl:call-template name="Footer" />
      </body>
    </html>
  </xsl:template>


  <xsl:template name="Footer">
    <div class="page-breakLC">
      <div class="centerLC">
        <h2 style="margin-bottom:0px;">Lethbridge College</h2>
        <h2 style="margin-bottom:0px;">Transcript Legend</h2>
      </div>
      <div class="justifyLC">
        <p>Lethbridge College is a public post-secondary institution established in 1957 under the statutes of the Province of Alberta. Lethbridge College is a member of the Association of Canadian Community Colleges (ACCC)</p>
      </div>
      <div class="centerLC">
        <h3>GRADE SYSTEM</h3>
      </div>
      <div>
        <h4>Effective July 1, 2003</h4>
        <div style="float:left;width:49%;">
          <table>
            <thead class='boldLC'>
              <tr>
                <td>Grade</td>
                <td>Grade Point</td>
                <td>Explanation</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>A+</td>
                <td>4.0</td>
                <td>Excellent</td>
              </tr>
              <tr>
                <td>A</td>
                <td>4.0</td>
                <td>Excellent</td>
              </tr>
              <tr>
                <td>A-</td>
                <td>3.7</td>
                <td>Excellent</td>
              </tr>
              <tr>
                <td>B+</td>
                <td>3.3</td>
                <td>Good</td>
              </tr>
              <tr>
                <td>B</td>
                <td>3.0</td>
                <td>Good</td>
              </tr>
              <tr>
                <td>B-</td>
                <td>2.7</td>
                <td>Good</td>
              </tr>
              <tr>
                <td>C+</td>
                <td>2.3</td>
                <td>Satisfactory</td>
              </tr>
              <tr>
                <td>C</td>
                <td>2.0</td>
                <td>Satisfactory</td>
              </tr>
              <tr>
                <td>C-</td>
                <td>1.7</td>
                <td>Satisfactory</td>
              </tr>
              <tr>
                <td>D+</td>
                <td>1.3</td>
                <td>Minimal Pass</td>
              </tr>
              <tr>
                <td>D</td>
                <td>1.0</td>
                <td>Minimal Pass</td>
              </tr>
              <tr>
                <td>F</td>
                <td>0.0</td>
                <td>Failure</td>
              </tr>
              <tr>
                <td colspan='3' class="boldLC">Effective July 1, 2014</td>
              </tr>
              <tr>
                <td>P</td>
                <td>Nil</td>
                <td>Pass</td>
              </tr>
              <tr>
                <td colspan='3' class="boldLC">Effective July 1, 2015</td>
              </tr>
              <tr>
                <td>0-100</td>
                <td>Nil</td>
                <td>Percentage</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div style="float:right;width:49%;">
          <table>
            <thead class="boldLC">
              <tr>
                <td>Symbol</td>
                <td>Grade Point</td>
                <td>Explanation</td>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>AF</td>
                <td>0.0</td>
                <td>Administrative Fail</td>
              </tr>
              <tr>
                <td>AI</td>
                <td>Nil</td>
                <td>Administrative Incomplete</td>
              </tr>
              <tr>
                <td>AS</td>
                <td>Nil</td>
                <td>Advance Standing</td>
              </tr>
              <tr>
                <td>AUD</td>
                <td>Nil</td>
                <td>Audit</td>
              </tr>
              <tr>
                <td>CR</td>
                <td>Nil</td>
                <td>Course Requirement Satisfied</td>
              </tr>
              <tr>
                <td>DF</td>
                <td>Nil</td>
                <td>Deferred Exam</td>
              </tr>
              <tr>
                <td>I</td>
                <td>Nil</td>
                <td>Incomplete (Interim Grade)</td>
              </tr>
              <tr>
                <td>IP</td>
                <td>Nil</td>
                <td>In Progress</td>
              </tr>
              <tr>
                <td>NCR</td>
                <td>Nil</td>
                <td>Course Requirement Not Satisfied</td>
              </tr>
              <tr>
                <td>NS</td>
                <td>Nil</td>
                <td>No Show</td>
              </tr>
              <tr>
                <td>PLC</td>
                <td>Nil</td>
                <td>Prior Learning Credit</td>
              </tr>
              <tr>
                <td>RW</td>
                <td>0.0</td>
                <td>Required to Withdraw</td>
              </tr>
              <tr>
                <td>WC</td>
                <td>Nil</td>
                <td>Withdrawal for Cause</td>
              </tr>
              <tr>
                <td>TR</td>
                <td>Nil</td>
                <td>Transfer Credit</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
      <div style="clear:both;"></div>
      <div>
        <p>Transcript records prior to 1985 are maintained in paper format only.</p>
        <p>
          Lethbridge College Academic Policies – Grading Procedures – Includes Grading for Credit Courses, Grade Point Average, Academic Standing, Recognition of Prior Learning(RPL), Academic Credits for Participation in Student Government, and definitions <br/> <a href="https://lethbridgecollege.ca/document-centre/grading-procedures" target="_blank">https://lethbridgecollege.ca/document-centre/grading-procedures</a>
        </p>
        <p>
          Lethbridge College Calendars and Course Descriptions –<br /> <a href="https://lethbridgecollege.ca/document-centre/publications/academic-calendar" target="_blank">https://lethbridgecollege.ca/document-centre/publications/academic-calendar</a>
        </p>
      </div>
      <br />
      <div class="boldLC centerLC">
        <p>Paper Transcript Records will be sent via Post</p>
      </div>
      <div class="boldLC centerLC" style="margin-top:10px;">
        <p>
          <span style="font-size:18px;">Lethbridge College</span>
        </p>
        <p>3000 College Drive South • Lethbridge • AB • T1K-1L6 • PH:(403) 320-3200 • Email: info@lethbridgecollege.ca</p>
      </div>
    </div>
  </xsl:template>

  <xsl:template name="htmlHead">
    <title>Lethbridge College Trancript</title>
    <style type="text/css" media="print">
      .mainContainerLC {
      width:100%;
      max-width:840px;
      margin:0;
      float:none;
      padding-top:0px;
      padding-left:0px;
      }
    </style>
    <style type="text/css">
      hr {
      height:1px;
      border:inset 1px #000000;
      }
      hr.thick {
      height:1px;
      border:inset 2px #000000;
      }
      td {
      overflow: visible;
      white-space: nowrap;
      }
      table {
      width:100%;
      }
      .programLC {
      padding-left:30px;
      }
      .courseCodeLC {
      padding-left: 45px;
      }
      .mainContainerLC {
      width:100%;
      max-width:1024px;
      margin:0;
      float:none;
      padding-top:0px;
      padding-left:0px;
      }
      .boldLC {
      font-weight:bold;
      }
      .centerLC {
      text-align:center;
      }
      .justifyLC {
      text-align:justify;
      }
      .rightLC {
      text-align:right;
      }
      .leftLC {
      text-align:left;
      }
      #courseIdCodeLC {
      width:18%;
      }
      #courseTitleLC {
      width:42%;
      }
      #courseCreditLC {
      font-size:12px;
      width:5.5%;
      }
      #courseCreditEarnedLC {
      font-size:12px;
      text-align:right;
      width:5.5%;
      }
      #courseGradeLC {
      font-size:12px;
      width:5.5%;
      }
      #coursePointsEarnedLC {
      font-size:12px;
      width:5.5%;
      }

      @media screen {
      .page-breakLC { margin-top: 20px; margin-bottom: 0px; border-bottom: 1px dashed #8c8b8b; }
      }

      @media print {
      .page-breakLC { page-break-before:always; margin-bottom: 20px; margin-bottom: 0px; }
      }
    </style>
  </xsl:template>

  <xsl:template name="documentHeader" match="Person">
    <div class="mainContainerLC">
      <table style="table-layout:fixed;">
        <tbody>
          <tr>
            <td>
              <img style="width:130px; padding-left:15px;" src="https://orion.lethbridgecollege.ca/prod/styles/images/LC_LOGO.jpg"  />
            </td>
            <td class="centerLC" style="vertical-align:center;" colspan="2">
              <h1 id="header" style="padding-top:30px; font-size:16px;">Official Academic Record</h1>
            </td>
            <td colspan="2">
              <div style="font-size:10px;">
                <div>
                  <span style="width:32%; display:inline-block;">
                    <b>Generated: </b>
                  </span>
                  <span class="rightLC" style="width:66.5%; display:inline-block; overflow:hidden; vertical-align:bottom;">
                    <xsl:value-of select="concat(substring(//../../TransmissionData/CreatedDateTime, 9, 2), '-')"/>
                    <xsl:choose>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '01'">Jan</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '02'">Feb</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '03'">Mar</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '04'">Apr</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '05'">May</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '06'">Jun</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '07'">Jul</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '08'">Aug</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '09'">Sep</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '10'">Oct</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '11'">Nov</xsl:when>
                      <xsl:when test="substring(//../../TransmissionData/CreatedDateTime, 6, 2) = '12'">Dec</xsl:when>
                    </xsl:choose>
                    <xsl:value-of select="concat('-', substring(//../../TransmissionData/CreatedDateTime, 1, 4))"/>
                  </span>
                </div>
                <div>
                  <span style="width:32%; display:inline-block; ">
                    <b>Tracking ID: </b>
                  </span>
                  <span class="rightLC" style="width:66.5%; display:inline-block; overflow:hidden; vertical-align:bottom;">
                    <xsl:value-of select="//../../TransmissionData/RequestTrackingID"/>
                  </span>
                </div>
                <div>
                  <span style="width:32%; display:inline-block;">
                    <b>Document ID: </b>
                  </span>
                  <span class="rightLC" style="width:66.5%; display:inline-block; overflow:hidden; vertical-align:bottom;">
                    <xsl:value-of select="//../../TransmissionData/DocumentID"/>
                  </span>
                </div>
              </div>
            </td>
          </tr>
          <tr style="height:28px;"></tr>
          <tr>
            <td colspan="2">
              <b>Student: </b>
              <xsl:value-of select="concat(//Name/FirstName,' ', //Name/MiddleName, ' ',//Name/LastName,' ')"/>
            </td>
            <td>
              <b>Student ID: </b>
              <xsl:value-of select="//SchoolAssignedPersonID"/>
            </td>
            <td>
              <b>Date of Birth: </b>
              <xsl:choose>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '01'">Jan</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '02'">Feb</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '03'">Mar</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '04'">Apr</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '05'">May</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '06'">Jun</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '07'">Jul</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '08'">Aug</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '09'">Sep</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '10'">Oct</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '11'">Nov</xsl:when>
                <xsl:when test="substring(//Birth/BirthDate, 6, 2) = '12'">Dec</xsl:when>
              </xsl:choose>-<xsl:value-of select="substring(//Birth/BirthDate, 9, 2)"/>
            </td>
            <td class="rightLC">
              <b>ASN: </b>
              <xsl:value-of select="//AgencyAssignedID"/>
            </td>
          </tr>
        </tbody>
      </table>
      <hr class="thick" />
    </div>
  </xsl:template>

  <xsl:template name="transcriptDetailsGrid">
    <div class="mainContainerLC">
      <xsl:call-template name="transferComments" />
      <div>
        <table border="0">
          <thead>
            <tr>
              <th id="courseIdCodeLC"></th>
              <th id="courseTitleLC"></th>
              <th id="courseCreditLC"></th>
              <th id="courseCreditEarnedLC">CRD</th>
              <th id="courseGradeLC">GRD</th>
              <th id="coursePointsEarnedLC">GRDPT</th>
            </tr>
          </thead>
          <tbody>

            <!-- Get All sessions/terms -->
            <xsl:apply-templates select="//AcademicSession" />

          </tbody>
        </table>
      </div>
      <div>
        <xsl:apply-templates select="//AcademicRecord/AcademicAward" />
      </div>
      <hr class="thick" />
      <div class="centerLC">*** End of Transcript ***</div>
    </div>
  </xsl:template>

  <xsl:template name="transferComments">
    <div>
      <xsl:for-each select="//AcademicRecord/NoteMessage">
        <xsl:value-of select="./text()"/>
      </xsl:for-each>
    </div>
  </xsl:template>

  <xsl:template  match="//AcademicRecord/AcademicAward">
    <div style="width:700px;">
      <xsl:if test="./AcademicHonors" >
        <p>
          <span class="boldLC">Honors: </span>
          <xsl:value-of select="./AcademicHonors/HonorsTitle"/>
        </p>
      </xsl:if>
      <p>
        <span class="boldLC">Credential Awarded: </span>
        <xsl:value-of select="./AcademicAwardTitle"/>
      </p>
      <p>
        <span class="boldLC">Date Awarded: </span>
        <!-- <xsl:value-of select="./AcademicAwardDate"/> -->

        <!-- <xsl:value-of select="concat(substring(./AcademicAwardDate, 9, 2), '-')"/> -->
        <xsl:choose>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '01'">Jan</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '02'">Feb</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '03'">Mar</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '04'">Apr</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '05'">May</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '06'">Jun</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '07'">Jul</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '08'">Aug</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '09'">Sep</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '10'">Oct</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '11'">Nov</xsl:when>
          <xsl:when test="substring(./AcademicAwardDate, 6, 2) = '12'">Dec</xsl:when>
        </xsl:choose>
        <xsl:value-of select="concat('-', substring(./AcademicAwardDate, 1, 4))"/>

      </p>
      <xsl:if test="./NoteMessage" >
        <p>
          <span class="boldLC">Award Comments: </span>
          <xsl:value-of select="./NoteMessage"/>
        </p>
      </xsl:if>
    </div>
    <hr />
  </xsl:template>

  <xsl:template match="//AcademicSession" >
    <tr>
      <td colspan="6">
        <b>
          <xsl:value-of select="./AcademicSessionDetail/SessionName"/>
        </b>
      </td>
    </tr>

    <tr>
      <td class="programLC" colspan="6">
        <b>
          <xsl:value-of select="./AcademicProgram/AcademicProgramName" />
        </b>
      </td>
    </tr>

    <tr>
      <td colspan="6" style="font-size:12px; white-space: pre-line;">
        <xsl:for-each select="./NoteMessage">
          <xsl:value-of select="./text()"/>
        </xsl:for-each>
      </td>
    </tr>

    <xsl:for-each select="./Course" >
      <xsl:for-each select=".">
        <tr>
          <td class="courseCodeLC" >
            <xsl:value-of select="./CourseSubjectAbbreviation"/>
            <xsl:value-of select="./CourseNumber"/>
          </td>
          <td>
            <xsl:value-of select="./CourseTitle"/>
          </td>
          <td class="rightLC" style="padding-right:7px;">
            <!-- <xsl:value-of select="format-number(./CourseCreditValue, '0.00')"/> -->
          </td>
          <td class="rightLC" style="padding-right:7px;">
            <xsl:choose>
              <xsl:when test="number(./CourseCreditEarned) or ./CourseCreditEarned = 0">
                <xsl:value-of select="format-number(./CourseCreditEarned, '0.00')"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="./CourseCreditEarned"/>
              </xsl:otherwise>
            </xsl:choose>
          </td>
          <td class="leftLC" style="padding-left:15px;">
            <xsl:value-of select="./CourseAcademicGrade"/>
          </td>
          <td class="rightLC" style="padding-right:7px;">
            <xsl:choose>
              <xsl:when test="number(./CourseQualityPointsEarned) or ./CourseQualityPointsEarned = 0">
                <xsl:value-of select="format-number(./CourseQualityPointsEarned, '0.00')"/>
              </xsl:when>
              <xsl:otherwise>
                <xsl:value-of select="./CourseQualityPointsEarned"/>
              </xsl:otherwise>
            </xsl:choose>
          </td>
        </tr>
      </xsl:for-each>
    </xsl:for-each>

    <!-- Determine whether student is on academic probation for this term -->

    <xsl:if test="../AcademicSummary/Delinquencies = 'ProbationGPA'">
      <tr style="height:30px; ">
        <td class="programLC">
          <b>
            Academic Probation
          </b>
        </td>
      </tr>
    </xsl:if>

    <!-- Total the grades only for this term -->

    <xsl:if test="./AcademicSummary/GPA" >
      <tr class="boldLC" style="height:15px; vertical-align:bottom;">
        <td>
        </td>
        <td>
          <span style="font-size:12px;">Term GPA: </span>
          <xsl:choose>
            <xsl:when test="number(./AcademicSummary/GPA/GradePointAverage) or ./AcademicSummary/GPA/GradePointAverage = 0">
              <xsl:value-of select="format-number(./AcademicSummary/GPA/GradePointAverage, '0.000')"/>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="./AcademicSummary/GPA/GradePointAverage"/>
            </xsl:otherwise>
          </xsl:choose>
        </td>
        <td class="rightLC">
          <span style="font-size:12px;">Credit: </span>
        </td>
        <td class="centerLC">
          <xsl:choose>
            <xsl:when test="number(./AcademicSummary/GPA/CreditHoursEarned) or ./AcademicSummary/GPA/CreditHoursEarned = 0">
              <xsl:value-of select="format-number(./AcademicSummary/GPA/CreditHoursEarned, '0.00')"/>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="./AcademicSummary/GPA/CreditHoursEarned"/>
            </xsl:otherwise>
          </xsl:choose>
        </td>
        <td class="rightLC">
        </td>
        <td class="centerLC">
        </td>
      </tr>
    </xsl:if>

    <xsl:if test="./AcademicSummary/AcademicHonors" >
      <tr class="boldLC" style="height:15px; vertical-align:bottom;">
        <td>
        </td>
        <td colspan="5" style="font-size:12px;">
          <span>Term Academic Standing: </span>
          <xsl:value-of select="./AcademicSummary/AcademicHonors/HonorsTitle"/>
        </td>
      </tr>
    </xsl:if>

    <tr style="height:20px;">
      <td colspan="6">
        <hr />
      </td>
    </tr>

  </xsl:template>

</xsl:stylesheet>