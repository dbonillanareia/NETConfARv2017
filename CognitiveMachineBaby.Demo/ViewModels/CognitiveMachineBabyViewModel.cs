using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace CognitiveMachineBaby.ViewModels
{
    public class CognitiveMachineBabyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		public ICommand MakeAnalysisCommand => new Command(async () => await MakeAnalysisRequest());
		
        private string imageUrl;
        public string ImageUrl 
        {
			get { return imageUrl; }
			set
			{
				imageUrl = value;
				OnPropertyChanged();
			}
        }

		private string detail;
		public string Detail
		{
			get { return detail; }
			set
			{
				detail = value;
				OnPropertyChanged();
			}
		}

		private string caption;
		public string Caption
		{
			get { return caption; }
			set
			{
				caption = value;
				OnPropertyChanged();
			}
		}

		private double? confidence;
		public double? Confidence
		{
			get { return confidence; }
			set
			{
				confidence = value;
				OnPropertyChanged();
			}
		}

        public ObservableCollection<string> Celebrities { get; set; } = new ObservableCollection<string>();

		const string subscriptionKey = "ba2a4158ba9a43ae8faa797592dfe8d7";
        const string uriBase = "https://eastus2.api.cognitive.microsoft.com/vision/v1.0";

		private async Task MakeAnalysisRequest()
		{
			var features = new VisualFeature[] { VisualFeature.Description };
            var details = new string[] { "Celebrities" };
            var visionClient = new VisionServiceClient(subscriptionKey, uriBase);
			var analysisResult = await visionClient.AnalyzeImageAsync(ImageUrl, features, details);


            Detail = analysisResult.Categories[0].Detail.ToString();
            Caption = analysisResult?.Description?.Captions[0]?.Text;
            Confidence = analysisResult?.Description?.Captions[0]?.Confidence;
		}
    }
}
