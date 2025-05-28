
function saveAsFile(filename, contentBase64) {
	const link = document.createElement('a');
	link.href = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + contentBase64;
	link.download = filename;
	document.body.appendChild(link);
	link.click();
	document.body.removeChild(link);
}
