<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ColTrn="urn:org:pesc:message:CollegeTranscript:v1.6.0" exclude-result-prefixes="ColTrn">
  <xsl:output method="html" doctype-public="-//W3C//DTD HTML 4.01//EN" doctype-system="http://www.w3.org/TR/html4/strict.dtd" indent="yes"/>
  <xsl:include href="../APAS/Functions.xsl"/>

	<!-- MyCreds XSL -->
	<xsl:template match="/">
		<html>
			<head>
				<xsl:call-template name="htmlHead" />
			</head>
			<body style="font-size:13px; width:auto; ">
				<xsl:call-template name="documentHeader" />
				<xsl:call-template name="transcriptDetailsGrid" />
				<!-- <xsl:call-template name="Footer" /> -->
			</body>
		</html>
	</xsl:template>

	<xsl:template name="htmlHead">
		<title>Lethbridge College Trancript</title>
		<style type="text/css" media="print">
			.mainContainerLC {
				width:100%;
				max-width:860px;
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
					<!--<tr style="height:200px;"></tr>-->
					<tr>
						<td>
							<!-- <img style="width:130px; padding-left:15px;" src="https://orion.lethbridgecollege.ca/prod/styles/images/LC_LOGO.jpg"  /> -->
						</td>
						<td class="centerLC" style="vertical-align:center;" colspan="2">
							<!-- <h1 id="header" style="padding-top:30px; font-size:16px;">Official Academic Record</h1> -->
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
							<xsl:if test="//AgencyAssignedID" >
								<b>ASN: </b>
								<xsl:value-of select="//AgencyAssignedID"/>
							</xsl:if>
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
			<!-- <hr class="thick" /> -->
			<!-- <div class="centerLC">*** End of Transcript ***</div> -->
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
			<xsl:if test="./AcademicAwardProgram/AcademicProgramName" >
				<p>
					<span class="boldLC">Program Awarded: </span>
					<xsl:value-of select="./AcademicAwardProgram/AcademicProgramName"/>
				</p>
			</xsl:if>
			<xsl:if test="./AcademicAwardTitle" >
				<p>
					<span class="boldLC">Credential Awarded: </span>
					<xsl:value-of select="./AcademicAwardTitle"/>
				</p>
			</xsl:if>
			<xsl:if test="./AcademicAwardDate" >
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
			</xsl:if>
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
