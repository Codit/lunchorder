export class ErrorDescriptor implements Serializable<ErrorDescriptor> {
        title: string;
        description: string;
        
        toString() : string {
            if (this.title) {
                return `${this.title} - ${this.description}`
            }
            else { return this.description };
        }

        deserialize(input : any) : ErrorDescriptor {
            if (input._body) {
                var bodyError = JSON.parse(input._body);
                this.title = bodyError.error;
                this.description = bodyError.error_description;
            }
            else if (input.message) {
                this.description = input.message;
            }            
            return this;
        }
}