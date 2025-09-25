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
  mobile: string;
}
