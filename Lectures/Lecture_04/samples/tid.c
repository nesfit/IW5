#define _GNU_SOURCE

#include <stdlib.h>
#include <stdio.h>
#include <errno.h>

#include <error.h>
#include <pthread.h>
#include <unistd.h>
#include <sys/syscall.h>

void *print_tid(void *arg)
{
	(void) arg;
	pid_t tid = (pid_t) syscall(SYS_gettid);
	printf("%d\n", tid);
	return NULL;
}

int main(int argc, char *argv[])
{
	if (argc != 2) {
		error(EXIT_FAILURE, 0, "a numeric argument is required");
	}
	char *end;
	long count = strtol(argv[1], &end, 10);
	if (*end || count <= 0) {
		error(EXIT_FAILURE, 0, "the argument must be a positive integer");
	}
	pthread_t *threads = malloc(count * sizeof(pthread_t));
	if (!threads) {
		error(EXIT_FAILURE, errno, "malloc");
	}
	for (size_t i = 0; i < count; ++i) {
		int perrno;
		if ((perrno = pthread_create(&threads[i], NULL, print_tid, NULL)) != 0) {
			free(threads);
			error(EXIT_FAILURE, perrno, "pthread_create(%lu)", i);
		}
	}
	for (size_t i = 0; i < count; ++i) {
		int perrno;
		if ((perrno = pthread_join(threads[i], NULL)) != 0) {
			free(threads);
			error(EXIT_FAILURE, perrno, "pthread_join(%lu)", i);
		}
	}
	puts("main exits");
	free(threads);
	return EXIT_SUCCESS;
}
