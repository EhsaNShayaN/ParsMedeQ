export interface SendOtpRequest {
  mobile: string;
}

export interface SendOtpResponse {
  otp?: string;
}

export interface MobileRequest {
  mobile: string;
  otp: string;
}

export interface MobileResponse {
  token: string;
  fullname: string;
  mobile: string;
}

export interface Profile {
  fullName: string;
  firstName: string;
  lastName: string;
}
