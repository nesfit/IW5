#include <stdio.h>

int main(void)
{
	long number = 0;
	int numberCount = 0;
	while ((numberCount = scanf(" %ld", &number)) == 1) {
		for (size_t i = 0; i < sizeof(long); ++i) {
			printf("%02x ", (unsigned char)((number >> i * 8) & 0xff));
		}
		printf("\n");
	}
	if (numberCount != EOF) {
		fprintf(stderr, "Invalid number\n");
		return 1;
	}

	return 0;
}
