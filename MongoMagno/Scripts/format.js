var defaultCultureName = new String();

function setCulture(name){
	defaultCultureName = name;
}

String.prototype.format = function () {
	var formatted = unescape(this).replace('&amp;', '&');
	for (var i = 0; i < arguments.length; i++) {
		var regexp = new RegExp('\\{' + i + '\\}', 'gi');
		formatted = formatted.replace(regexp, arguments[i] == null ? '' : arguments[i].toString());
	}
	return formatted;
}

String.map = function(str, src, dest) {
	var result = new String();
	for(var i = 0; i < str.length; i++) {
		var index = src.indexOf(str.charAt(i));
		if(index != -1) {
			result += dest.charAt(index);
		}
		else {
			result += str.charAt(i);
		}
	}
	return result;
}

invariantFormatProvider = function (obj, format) {	
	if (format) {		
		var result = new String();
		var raw = new String();
		switch (typeof obj) {
			case 'number':
				raw = obj.toString();				
				var numParts = raw.split('.');				
				var re = /(0*)(#*)\.?(#*)(0*)/g;
				var parts = re.exec(format);
				
				if(parts[1] != '' && (parts[1].length + parts[2].length > numParts[0].length)) {
					result += Array(parts[1].length + parts[2].length - numParts[0].length + 1).join('0');
				}
				
				result += numParts[0];
												
								
				if(numParts.length == 1) {
					numParts[1] = '';
				}
				
				if(parts[3] != '' && parts[3].length < numParts[1].length) {
					numParts[1] = numParts[1].substr(0, parts[3].length);
				}
				if((parts[4] != '') || (numParts[1] != '')) {
					result += '.' + numParts[1];
				}				
				
				if((parts[4] != '') && (parts[3].length + parts[4].length > numParts[1].length)) {				
					result += Array(parts[3].length + parts[4].length - numParts[1].length + 1).join('0');
				}
				
				return result;
			case 'object':
				if(obj instanceof Date){
					var re = /y{2,4}/;					
					var result = format;
					var match = format.match(re);
					if(match) {					
						if(match[0].length == 4) {
							result = result.replace(re, obj.getFullYear());
						}
						else{
							result = result.replace(re, obj.getFullYear().toString().substr(2, 2));
						}
					}
					re = /M{1,2}/;
					match = format.match(re);
					if(match) {
						if(match[0].length == 2) {
							result = result.replace(re, invariantFormatProvider(obj.getMonth(),'00'));
						}
						else{
							result = result.replace(re, obj.getMonth());
						}
					}
					re = /d{1,2}/;
					match = format.match(re);
					if(match) {
						if(match[0].length == 2) {
							result = result.replace(re, invariantFormatProvider(obj.getDay(),'00'));
						}
						else{
							result = result.replace(re, obj.getDay());
						}
					}
					re = /[hH]{1,2}/;
					match = format.match(re);
					if(match) {
						var hour = obj.getHours();
						if(match[0].indexOf('h') != -1) {
							if(hour > 12) hour -= 12;
						}
						if(match[0].length == 2) {
							result = result.replace(re, invariantFormatProvider(hour,'00'));
						}
						else{
							result = result.replace(re, hour);
						}
					}
					re = /m{1,2}/;
					match = format.match(re);
					if(match) {
						if(match[0].length == 2) {
							result = result.replace(re, invariantFormatProvider(obj.getMinutes(),'00'));
						}
						else{
							result = result.replace(re, obj.getMinutes());
						}
					}
					re = /s{1,2}/;
					match = format.match(re);
					if(match) {
						if(match[0].length == 2) {
							result = result.replace(re, invariantFormatProvider(obj.getSeconds(),'00'));
						}
						else{
							result = result.replace(re, obj.getSeconds());
						}
					}					
					return result;
				}
			default:
				return obj.toString();
		}
	}
	else {
		return obj.toString();
	}	
}

cultureFormatProvider = function (obj, cultureName, format) {
	if (obj instanceof Date && !format) {
		format = cultures[cultureName].defaultDateFormat;		
	}
	if (format) {
		var result = new String();
		var raw = new String();
		switch (typeof obj) {
			case 'number':
				raw = obj.toString();
				var numParts = raw.split('.');
				var re = /(0*)(#*)\.?(#*)(0*)/g;
				var parts = re.exec(format);
				if (parts[1] != '' && (parts[1].length + parts[2].length > numParts[0].length)) {
					result += Array(parts[1].length + parts[2].length - numParts[0].length + 1).join(cultures[cultureName].numbers[0]);
				}

				result += String.map(numParts[0], cultures.invariant.numbers, cultures[cultureName].numbers);


				if (numParts.length == 1) {
					numParts[1] = '';
				}

				if (parts[3].length < numParts[1].length) {
					numParts[1] = numParts[1].substr(0, parts[3].length);
				}
				if ((parts[4] != '') || (numParts[1] != '')) {
					result += cultures[cultureName].decimal + String.map(numParts[1], cultures.invariant.numbers, cultures[cultureName].numbers);
				}


				if ((parts[4] != '') && (parts[3].length + parts[4].length > numParts[1].length)) {
					result += Array(parts[3].length + parts[4].length - numParts[1].length + 1).join(cultures[cultureName].numbers[0]);
				}

				return result;
			case 'object':
				if (obj instanceof Date) {
					var re = /y{2,4}/;
					var result = format;
					var match = format.match(re);
					var localeDate = cultures[cultureName].fromDate(obj);
					if (match) {
						if (match[0].length == 4) {
							result = result.replace(re, cultureFormatProvider(localeDate.year, cultureName));
						}
						else {
							result = result.replace(re, cultureFormatProvider(localeDate.year, cultureName).substr(2, 2));
						}
					}
					re = /M{1,2}/;
					match = format.match(re);
					if (match) {
						if (match[0].length == 2) {
							result = result.replace(re, cultureFormatProvider(localeDate.month, cultureName, '00'));
						}
						else {
							result = result.replace(re, cultureFormatProvider(localeDate.month, cultureName));
						}
					}
					re = /d{1,2}/;
					match = format.match(re);
					if (match) {
						if (match[0].length == 2) {
							result = result.replace(re, cultureFormatProvider(localeDate.day, cultureName, '00'));
						}
						else {
							result = result.replace(re, cultureFormatProvider(localeDate.day, cultureName));
						}
					}
					re = /[hH]{1,2}/;
					match = format.match(re);
					if (match) {
						var hour = obj.getHours();
						if (match[0].indexOf('h') != -1) {
							if (hour > 12) hour -= 12;
						}
						if (match[0].length == 2) {
							result = result.replace(re, cultureFormatProvider(hour, cultureName, '00'));
						}
						else {
							result = result.replace(re, cultureFormatProvider(hour, cultureName));
						}
					}
					re = /m{1,2}/;
					match = format.match(re);
					if (match) {
						if (match[0].length == 2) {
							result = result.replace(re, cultureFormatProvider(obj.getMinutes(), cultureName, '00'));
						}
						else {
							result = result.replace(re, cultureFormatProvider(obj.getMinutes(), cultureName));
						}
					}
					re = /s{1,2}/;
					match = format.match(re);
					if (match) {
						if (match[0].length == 2) {
							result = result.replace(re, cultureFormatProvider(obj.getSeconds(), cultureName, '00'));
						}
						else {
							result = result.replace(re, cultureFormatProvider(obj.getSeconds(), cultureName));
						}
					}
					return result;
				}
			default:
				return String.map(obj.toString(), cultures.invariant.numbers, cultures[cultureName].numbers);
		}
	}
	else {
		return String.map(obj.toString(), cultures.invariant.numbers, cultures[cultureName].numbers);
	}
}

String.cultureFormat = function (culture, format) {
	try {
		var formatProvider = function (obj, format) {
			if (culture && (culture != ''))
				return cultureFormatProvider(obj, culture, format);
			else
				return invariantFormatProvider(obj, format);
		};

		var formatted = unescape(format).replace('&amp;', '&');
		if (arguments[2] instanceof Array) {
			var array = arguments[2];
			for (var i = 0; i < array.length; i++) {
				var regexp = new RegExp('\\{' + i + ':?(.*?)\\}', 'gi');
				var match = regexp.exec(formatted);
				if (match && match.length > 0)
					formatted = formatted.replace(regexp, array[i] == null ? '' : formatProvider(array[i], match[1]));
			}
		}
		else if ((typeof arguments[2] != 'number') && (typeof arguments[2] != 'string') && !(arguments[2] instanceof Date)) {
			var obj = arguments[2];
			for (var i in obj) {
				var regexp = new RegExp('@\\{' + i + ':?(.*?)\\}', 'gi');
				var match = regexp.exec(formatted);
				if (match && match.length > 0)
					formatted = formatted.replace(regexp, obj[i] == null ? '' : formatProvider(obj[i], match[1]));
			}
		}
		else {
			for (var i = 2; i < arguments.length; i++) {
				var regexp = new RegExp('\\{' + (i - 2).toString() + ':?(.*?)\\}', 'gi');
				var match = regexp.exec(formatted);
				if (match && match.length > 0)
					formatted = formatted.replace(regexp, arguments[i] == null ? '' : formatProvider(arguments[i], match[1]));
			}
		}
	}
	catch (error) {
		format = error.message;
	}
	return formatted;
}


String.format = function (format) {
	try {
		var formatProvider = function (obj, format) {
			if (defaultCultureName && (defaultCultureName != ''))
				return cultureFormatProvider(obj, defaultCultureName, format);
			else
				return invariantFormatProvider(obj, format);
		};
		var formatted = unescape(format).replace('&amp;', '&');
		if (arguments[1] instanceof Array) {
			var array = arguments[1];
			for (var i = 0; i < array.length; i++) {
				var regexp = new RegExp('\\{' + i + ':?(.*?)\\}', 'gi');
				var match = regexp.exec(formatted);
				if (match && match.length > 0)
					formatted = formatted.replace(regexp, array[i] == null ? '' : formatProvider(array[i], match[1]));
				else
					formatted = 'Invalid format';
			}
		}
		else if ((typeof arguments[1] != 'number') && (typeof arguments[1] != 'string') && !(arguments[1] instanceof Date)) {
			var obj = arguments[1];
			for (var i in obj) {
				var regexp = new RegExp('@\\{' + i + ':?(.*?)\\}', 'gi');
				var match = regexp.exec(formatted);
				if (match && match.length > 0)
					formatted = formatted.replace(regexp, obj[i] == null ? '' : formatProvider(obj[i], match[1]));
				else
					formatted = 'Invalid format';
			}
		}
		else {
			for (var i = 1; i < arguments.length; i++) {
				var regexp = new RegExp('\\{' + (i - 1).toString() + ':?(.*?)\\}', 'gi');
				var match = regexp.exec(formatted);
				if (match && match.length > 0)
					formatted = formatted.replace(regexp, arguments[i] == null ? '' : formatProvider(arguments[i], match[1]));
				else
					formatted = 'Invalid format';
			}
		}
	}
	catch (error) {
		format = error.message;
	}
	return formatted;
}

