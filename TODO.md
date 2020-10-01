* Add tests for class coder
* Add list/array coder
* Add map coder
* Add boolean coder
* Add float/double/decimal coder
* Add signed integer coder
* Add datetime/timespan coder
* Add documentation, including transform
* Add VLQ-max-bytes, whereby there is no continuation bit on the last byte
* Consider "Group Varint Encoding" https://en.wikipedia.org/wiki/Variable-length_quantity
* Support class fields as well as properties?
* Consider precomputing or caching VLQ to improve computation time
* Publish on NuGet