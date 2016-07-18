declare module jasmine {
	interface Matchers {
      toHaveText(text: string): boolean;
			toContainText(text: string): boolean;
		}
}
